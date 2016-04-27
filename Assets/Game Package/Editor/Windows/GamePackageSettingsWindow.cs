using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Package
{
    public class GamePackageSettingsWindow : EditorWindow
    {
        private GamePackageSettings loadingSceneArray;

        [MenuItem("Window/Game Package")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<GamePackageSettingsWindow>();
        }
        
        void OnGUI()
        {
            if (loadingSceneArray == null)
            {
                loadingSceneArray = AssetDatabase.LoadAssetAtPath<GamePackageSettings>(Path.SETTINGS);
                if (loadingSceneArray == null)
                {
                    loadingSceneArray = new GamePackageSettings();
                    AssetDatabase.CreateAsset(loadingSceneArray, Path.SETTINGS);
                }
            }
            GUILayout.Label("Game Package Settings", EditorStyles.boldLabel);

            SerializedObject so = new SerializedObject(loadingSceneArray);
            SerializedProperty stringsProperty = so.FindProperty("loadingScene");

            EditorGUILayout.PropertyField(stringsProperty, true);
            so.ApplyModifiedProperties();
            
            
            AssetDatabase.SaveAssets();
        }
    }    
}
