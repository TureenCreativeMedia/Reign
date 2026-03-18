using UnityEngine;

namespace reign
{
    public class App : Singleton<App>, IDataHandler
    {
        public static System.Action Action_HangApplication;
        public AppData AppData_App;
        public long long_AppUnixTimestamp { get; private set; }

        public void Start()
        {
            long_AppUnixTimestamp = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
        public void SetScreenResolution(ScreenResolution resolution)
        {
            if(resolution.int_Width == -1 && resolution.int_Height == -1)
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, resolution.bool_Fullscreen);
            }
            else
            {
                Screen.SetResolution(resolution.int_Width, resolution.int_Height, resolution.bool_Fullscreen);
            }
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
