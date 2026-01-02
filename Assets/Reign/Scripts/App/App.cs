using UnityEngine;

namespace reign
{
    public class AppSettings
    {
        public string appname;
        public string versionname;
        public bool developmentbuild;
        public int targetrefreshrate;
        public bool vsync;
        public bool console;
        public bool appwidgets;
        public bool discord;
    }

    public static class App
    {
        public static AppSettings u_localdata;

        public static void InitializeAppData(TextAsset jsonFile)
        {
            AppSettings data = JsonUtility.FromJson<AppSettings>(jsonFile.text);
            u_localdata = data;

            SetRefreshRate();
        }

        public static void SetRefreshRate()
        {
            Resolution u_SavedResolution = new();
            u_SavedResolution.width = SaveSystem.u_PlayerData.u_ScreenResolution.width;
            u_SavedResolution.height = SaveSystem.u_PlayerData.u_ScreenResolution.height;
            u_SavedResolution.refreshRateRatio = SaveSystem.u_PlayerData.u_ScreenResolution.refreshRateRatio;
            
            Screen.SetResolution(u_SavedResolution.width, u_SavedResolution.height, FullScreenMode.MaximizedWindow, u_SavedResolution.refreshRateRatio);

            Application.targetFrameRate = (u_localdata.vsync == true) ? -1 : u_localdata.targetrefreshrate;
        }
    }
}