using UnityEngine;
using System.Collections;

namespace Game.Package
{
    [RequireComponent(typeof(SceneLoader))]
    public class SceneChangeTrigger : ActionTrigger
    {
        private SceneLoader sceneLoader;
        protected override void Awake()
        {
            base.Awake();
            sceneLoader = GetComponent<SceneLoader>();
        }

        protected override void ExecuteAction()
        {
            base.ExecuteAction();
            sceneLoader.Load();
        }
    }
}
