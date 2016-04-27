using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Game.Package
{
    public class SceneLoader : MonoBehaviour
    {
        public BuildingScenes scene;
        public UnityEvent preLoading, onLoading;

        public float delay = 0;
        public bool loadAsync = false;
        public bool showLoading = false;

        public void Load()
        {
            if (preLoading != null) preLoading.Invoke();

            if (delay == 0)
                ExecuteLoading(scene, showLoading);
            else
                StartCoroutine(CreateRoutine(delay, scene, showLoading));
        }

        private IEnumerator CreateRoutine(float waitTime, BuildingScenes scene, bool showLoading)
        {
            yield return new WaitForSeconds(waitTime);
            ExecuteLoading(scene, showLoading);
        }

        private void ExecuteLoading(BuildingScenes scene, bool showLoading)
        {
            if (onLoading != null) onLoading.Invoke();
            DestroyOldData();

            string sceneName;
            if (showLoading)
            {
                GamePackageSettings gamePackageSettings = GamePackageSettings.Load();
                BuildingScenes loadingScene = gamePackageSettings.loadingScene;
                sceneName = EnumExtantions.GetDescription(loadingScene);

                gameObject.Log("Creating middle gameObject");
                GameObject middleGameObject = new GameObject("LoadingMiddleObject");
                gameObject.Log("Adding Loading component");
                middleGameObject.AddComponent<Loading>().Scene = scene;
            }
            else sceneName = EnumExtantions.GetDescription(scene);

            gameObject.Log("Loading scene:" + sceneName);
#if UNITY_5_3
            if (loadAsync) UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            else UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
#else
            if (loadAsync) Application.LoadLevelAsync(sceneName);
            else Application.LoadLevel(sceneName);
#endif
        }

        private void DestroyOldData()
        {
            SceneTransferData transferData = FindObjectOfType<SceneTransferData>();
            if (transferData != null) Destroy(transferData.gameObject);
        }
    }
}
