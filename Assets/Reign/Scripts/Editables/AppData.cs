using UnityEngine;

namespace reign
{
    [CreateAssetMenu(menuName = "Reign/App/App Data")]
    public class AppData : ScriptableObject
    {
        public string string_AppName = "Reign App";
        public string string_AppVersion = "v1.0.0";
        public long long_DiscordAppID;
        public bool bool_DiscordRPC = false;
        public bool bool_DebugBuild = true;
        public bool bool_Screenshotting = true;
    }

}
