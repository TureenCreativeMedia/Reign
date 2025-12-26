using System;
using System.IO;
using UnityEngine;

namespace reign
{
    public class Screenshot : MonoBehaviour
    {
        private string s_ScreenshotDirectory;
        private void Awake()
        {
            s_ScreenshotDirectory = Path.Combine(Application.persistentDataPath, "Screenshots");
            Directory.CreateDirectory(s_ScreenshotDirectory);
        }   
        private void OnEnable()
        {
            Main.a_OnFrame += Check;
        }
        private void OnDisable()
        {
            Main.a_OnFrame -= Check;
        }

        void Check()
        {
            if(InputSystem.GetInput("F2", InputSystem.KeyType.Down))
            {
                Capture();
            }
        }

        void Capture()
        {
            string s_Filename = $"Screenshot_{DateTime.Now:yyyy-MM-dd_HH.mm.ss}.png";
            string s_FullPath = Path.Combine(s_ScreenshotDirectory, s_Filename);
            ScreenCapture.CaptureScreenshot(s_FullPath);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            Debug.Log($"Screenshot saved at {s_FullPath}");
        }
    }
}
