using UnityEngine;

namespace Game.Package
{
    public class Loading : MonoBehaviour
    {
        public BuildingScenes Scene { get { return scene; } set { scene = value; if (sceneLoader) sceneLoader.scene = value; } }
        private BuildingScenes scene;

        private float delay = 2;
        private SceneLoader sceneLoader;

        void Start()
        {
            gameObject.Log("Start");
            DontDestroyOnLoad(gameObject);
            sceneLoader = gameObject.AddComponent<SceneLoader>();
            sceneLoader.scene = scene;
            sceneLoader.delay = delay;
            sceneLoader.loadAsync = true;
        }

        void OnLevelWasLoaded(int level)
        {
            gameObject.Log("OnLevelWasLoaded");
            sceneLoader.Load();
            DestroyObject(gameObject, delay + 1);
        }
    }
}