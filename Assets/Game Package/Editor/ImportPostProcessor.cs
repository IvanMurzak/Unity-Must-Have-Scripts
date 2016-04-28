using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Game.Package
{
    public class ImportPostProcessor : AssetPostprocessor
    {
        private const string SCENE_FILE_FORMAT = ".unity";
        private static string[] EXAMPLE_SCENES = new string[] 
        {
            "ExampleAnimation",
            "ExampleAudio",
            "ExampleKiller",
            "ExampleSceneLoading",
            "ExampleTrigger",
            "AsyncLoadingScene"
        };
        
        static void AddExampleSceneToBuild(string sceneName)
        {
            EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length + 1];
            int index = 0;
            for (; index < EditorBuildSettings.scenes.Length; index++)
                scenes[index] = EditorBuildSettings.scenes[index];

            scenes[index++] = new EditorBuildSettingsScene(Path.EXAMPLES + sceneName + SCENE_FILE_FORMAT, true);

            EditorBuildSettings.scenes = scenes;
        }

        public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            Debug.Log("OnPostprocessAllAssets");
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            bool[] isAdded = new bool[EXAMPLE_SCENES.Length];
            for(int index = 0; index < isAdded.Length; index++)
            {
                isAdded[index] = false;
                for(int secondIndex = 0; secondIndex < scenes.Length; secondIndex++)
                {
                    string path = scenes[secondIndex].path;
                    string name = path.Substring(path.LastIndexOf('/') + 1);
                    name = name.Substring(0, name.Length - SCENE_FILE_FORMAT.Length);

                    if (name.Equals(EXAMPLE_SCENES[index]))
                    {
                        scenes[secondIndex].enabled = true;
                        isAdded[index] = true;
                        break;
                    }
                }
            }


            for (int index = 0; index < isAdded.Length; index++)
            {
                if (!isAdded[index])
                {
                    Debug.Log("OnPostprocessAllAssets: Adding - " + EXAMPLE_SCENES[index]);
                    AddExampleSceneToBuild(EXAMPLE_SCENES[index]);
                }
            }
        }
    }
}
