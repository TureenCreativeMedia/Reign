using UnityEngine;

namespace reign
{
    public struct OnHangApplication : IEvent { }
    public struct AttemptHangApplication : IEvent { }
    public class App : Singleton<App>, IDataHandler
    {
        public AppData AppData_App;
        public long long_AppUnixTimestamp { get; private set; }

        void OnEnable()
        {
            EventBus.Subscribe<AttemptHangApplication>(AttemptHangApplication);
        }
        
        void OnDisable()
        {
            EventBus.Unsubscribe<AttemptHangApplication>(AttemptHangApplication);
        }
        
        public void Start()
        {
            long_AppUnixTimestamp = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
        public void SetScreenResolution(ScreenResolution RESOLUTION)
        {
            Screen.brightness = RESOLUTION.float_Brightness;
            Application.targetFrameRate = RESOLUTION.bool_VSync ? Mathf.CeilToInt((float)Screen.currentResolution.refreshRateRatio.value) : RESOLUTION.int_Hz;
            QualitySettings.vSyncCount = RESOLUTION.bool_VSync ? 1 : 0;

            if((Screen.currentResolution.width != RESOLUTION.int_Width) || (Screen.currentResolution.height != RESOLUTION.int_Height))
            {    
                if(RESOLUTION.int_Width <= 0 && RESOLUTION.int_Height <= 0)
                {
                    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, RESOLUTION.bool_Fullscreen);
                }
                else
                {
                    Screen.SetResolution(RESOLUTION.int_Width, RESOLUTION.int_Height, RESOLUTION.bool_Fullscreen);
                }
            }
        }

        protected void AttemptHangApplication(AttemptHangApplication EVENT)
        {
            HangApplication();
        }

        public void HangApplication()
        {
            EventBus.Publish(new OnHangApplication { });
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