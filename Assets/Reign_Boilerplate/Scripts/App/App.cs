using UnityEngine;

namespace reign
{
    public class AppSettings
    {
        public string versionname;
        public bool developmentbuild;
    }

    public static class App
    {
        public static AppSettings u_localdata;

        public static void InitializeAppData(TextAsset jsonFile)
        {
            AppSettings data = JsonUtility.FromJson<AppSettings>(jsonFile.text);
            u_localdata = data;
        }
    }
}