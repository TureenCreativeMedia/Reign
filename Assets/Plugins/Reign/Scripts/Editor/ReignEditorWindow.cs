using Reign.Generic;
using UnityEditor;
using UnityEngine;

namespace Reign.Editor
{
    public class ReignEditorWindow : EditorWindow
    {

        [MenuItem("Reign/Reign Tools")]
        public static void ShowWindow()
        {
            GetWindow<ReignEditorWindow>($"Reign Tools ({ReignServiceDetails.REIGN_TOOLS_VERSION})");
        }

        private void OnGUI()
        {
            GUILayout.Label("Reign Tools", EditorStyles.whiteLargeLabel);

            GUILayout.Label($"Reign v{ReignServiceDetails.REIGN_VERSION} - Released {ReignServiceDetails.RELEASE_DATE}", EditorStyles.centeredGreyMiniLabel);

            GUILayout.Space(10);

            if (GUILayout.Button($"Add Reign Dependencies to Scene '{UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().name}'"))
            {
                SetupScene();
            }

            GUILayout.Space(10);

        }

        private void SetupScene()
        {
            CreateReignPrefabInstance();
        }

        private void CreateReignPrefabInstance()
        {
            if (FindAnyObjectByType<Reign>())
            {
                Debug.LogWarning("Reign prefab was already present in the scene.");
                return;
            }

            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Plugins/Reign/Prefabs/Reign.prefab");

            if (prefab == null)
            {
                Debug.LogError("Reign prefab not found!");
                return;
            }

            var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            Undo.RegisterCreatedObjectUndo(instance, "Spawn Reign Prefab");

            instance.name = prefab.name;
        }
    }
}
