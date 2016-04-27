using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Game.Package
{
    public class SceneLoader : MonoBehaviour
    {
        private const string LOADING_SCENE_NAME = "AsyncLoadingScene";

        public BuildingScenes scene;
        public UnityEvent preLoading, onLoading;

        public float delay = 0;
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
            string sceneName = showLoading ? LOADING_SCENE_NAME : EnumExtantions.GetDescription(scene);

#if UNITY_5_3
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
#else
        Application.LoadLevel(sceneName);
#endif
        }

        private void DestroyOldData()
        {
            SceneTransferData transferData = FindObjectOfType<SceneTransferData>();
            if (transferData != null) Destroy(transferData.gameObject);
        }
    }
}
