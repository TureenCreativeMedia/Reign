using System.IO;
using UnityEngine;

namespace reign
{
    public class ScreenshotSystem : BaseSystem, IUpdatable
    {
        protected string string_Directory => $"{Application.persistentDataPath}/Screenshots/{App.Instance.AppData_App.string_AppName}_Screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            
        void OnEnable()
        {
            if(!App.Instance.AppData_App.bool_Screenshotting) return;
            Initiate();
            UpdateSystem.Register(this);
        }
        void OnDisable()
        {
            UpdateSystem.Unregister(this);
        }
        void Initiate()
        {
            Directory.CreateDirectory($"{Application.persistentDataPath}/Screenshots/");
        }
        void IUpdatable.Tick(float DELTATIME)
        {
            if(InputSystem.GetInput("Screenshot", InputSystem.enum_KeyType.Down)) 
            {
                try
                {
                    ScreenCapture.CaptureScreenshot(string_Directory);
                    Logger.Instance.Log(Logger.enum_LogIntensity.Log, $"Saved screenshot to: {string_Directory}");
                }
                catch
                {
                    return;
                }
            }
        }
    }
}