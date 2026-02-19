using System;
using UnityEngine;

namespace reign
{
    public class App : Singleton<App>, IDataHandler, IOriginStart
    {
        public static System.Action Action_HangApplication;
        public AppData AppData_App;
        public long long_AppUnixTimestamp { get; private set; }

        public void OriginStart()
        {
            long_AppUnixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
        public void SetScreenResolution(ScreenResolution resolution)
        {
            Screen.SetResolution(resolution.int_Width, resolution.int_Height, resolution.bool_Fullscreen);
            Screen.brightness = resolution.float_Brightness;
            Application.targetFrameRate = resolution.bool_VSync ? (int)Screen.currentResolution.refreshRateRatio.value : resolution.int_Hz;
            QualitySettings.vSyncCount = resolution.bool_VSync ? 1 : 0;
        }
        public void HangApplication()
        {
            Action_HangApplication?.Invoke();
            Application.Quit();
        }
        public void LoadData(GameData DATA)
        {
            SetScreenResolution(DATA.ScreenResolution_Resolution);    
        }

        public void SaveData(ref GameData DATA)
        {
        }
    }
}
