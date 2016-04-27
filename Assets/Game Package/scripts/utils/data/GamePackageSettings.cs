using UnityEngine;
using System.Collections;

namespace Game.Package
{
    [System.Serializable]
    public class GamePackageSettings : ScriptableObject
    {
        public const string FILE_NAME = "GamePackageSettings";
        public BuildingScenes loadingScene = new BuildingScenes();

        public static GamePackageSettings Load()
        {
            return Resources.Load<GamePackageSettings>(FILE_NAME);
        }
    }
}
