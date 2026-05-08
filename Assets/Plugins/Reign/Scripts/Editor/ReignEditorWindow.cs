using Reign.Generic;
using Reign.Utility;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Reign.Editor
{
    public class ReignEditorWindow : EditorWindow
    {
        bool tryAddReign = true;
        bool tryCreateCam = true;

        [MenuItem("Reign/Reign Tools")]
        public static void ShowWindow()
        {
            GetWindow<ReignEditorWindow>($"Reign Tools");
        }

        private void OnGUI()
        {
            GUILayout.Label($"Reign Tools | v{ReignServiceDetails.REIGN_TOOLS_VERSION}", EditorStyles.whiteLargeLabel);

            GUILayout.Label($"Reign v{ReignServiceDetails.REIGN_VERSION} - Released {ReignServiceDetails.RELEASE_DATE}", EditorStyles.centeredGreyMiniLabel);

            GUILayout.Space(10);

            tryAddReign = GUILayout.Toggle(tryAddReign, "Try Add Reign Prefab");
            tryCreateCam = GUILayout.Toggle(tryCreateCam, "Try Add Camera");


            GUILayout.Space(10);

            if (GUILayout.Button($"Add Reign Dependencies to Scene '{EditorSceneManager.GetActiveScene().name}'"))
            {
                SetupScene();
            }

            GUILayout.Space(10);
        }

        private void SetupScene()
        {
            if (tryAddReign)
            {
                if (FindAnyObjectByType<Reign>())
                {
                    Debug.LogWarning("Reign prefab was already present in the scene.");
                }
                else
                {
                    ReignUtility.InstantiatePrefabInEditor("Assets/Plugins/Reign/Prefabs/Reign.prefab");
                }
            }

            if (tryCreateCam)
            {
                Camera[] cameras = FindObjectsByType<Camera>();

                if (cameras.Length == 0)
                {
                    var newCam = new GameObject("Camera").AddComponent<Camera>();
                    newCam.name = "Camera";
                    newCam.GetUniversalAdditionalCameraData().renderPostProcessing = true;
                }

                foreach (Camera cam in cameras)
                {
                    cam.GetUniversalAdditionalCameraData().renderPostProcessing = true;
                }
            }
        }
    }
}
