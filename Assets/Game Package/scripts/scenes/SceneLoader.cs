using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Game.Package
{
    public class SceneLoader : MonoBehaviour
    {
        public BuildingScenes scene;
        public UnityEvent onLoading;

        [Space]
        public bool loadAsync = false;
        public bool showLoading = false;

        [Space]
        public bool fade;
        public Color fadeTo = Color.black;
        public float fadeSpeed = 1.5f;
        
        private static SceneFadeInOut fader;
        private static SceneFadeInOut Fader {
            get
            {
                if (fader == null)
                {
                    fader = new GameObject("SceneFadeInOut").AddComponent<SceneFadeInOut>();
                    fader.ForceSetColor(Color.clear);
                    DontDestroyOnLoad(fader.gameObject);
                }
                return fader;
            }
        }

        public void Load()
        {
            if (fade)
            {
                Fader.onFadeComplete.RemoveAllListeners();
                Fader.onFadeComplete.AddListener(ExecuteLoading);
                Fader.fadeSpeed = fadeSpeed;
                Fader.FadeToColor(fadeTo);
            }
            else
            {
                ExecuteLoading(scene, showLoading);
            }
        }

        private void ExecuteLoading()
        {
            ExecuteLoading(scene, showLoading);
        }

        private void ExecuteLoading(BuildingScenes scene, bool showLoading)
        {
            Fader.onFadeComplete.RemoveAllListeners();
            if (onLoading != null) onLoading.Invoke();
            DestroyOldData();

            string sceneName;
            if (showLoading)
            {
                GamePackageSettings gamePackageSettings = GamePackageSettings.Load();
                BuildingScenes loadingScene = gamePackageSettings.loadingScene;
                sceneName = EnumExtantions.GetDescription(loadingScene);
                
                GameObject middleGameObject = new GameObject("LoadingMiddleObject");
                Loading loading = middleGameObject.AddComponent<Loading>();
                loading.Scene = scene;
                loading.fadeFrom = fadeTo;
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



        // -------------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------



        private class Loading : MonoBehaviour
        {
            public BuildingScenes Scene { get { return scene; } set { scene = value; if (sceneLoader) sceneLoader.scene = value; } }
            public Color fadeFrom;
            private BuildingScenes scene;

            private float delay = 2;
            private SceneLoader sceneLoader;
            private int sceneCounter = 0;

            void Start()
            {
                DontDestroyOnLoad(gameObject);
                sceneLoader = gameObject.AddComponent<SceneLoader>();
                sceneLoader.scene = scene;
                sceneLoader.fade = false;
                sceneLoader.loadAsync = true;
            }

            void OnLevelWasLoaded(int level)
            {
                if (sceneCounter == 0)
                {
                    Fader.FadeToClear();
                    StartCoroutine(CreateRoutine(level));
                }
                else
                {
                    Fader.ForceSetColor(fadeFrom);
                    Fader.FadeToClear();
                    DestroyObject(gameObject);
                }
                sceneCounter++;
            }

            private IEnumerator CreateRoutine(int level)
            {
                yield return new WaitForSeconds(delay);
                sceneLoader.Load();
                yield return null;
            }
        }
    }
}
