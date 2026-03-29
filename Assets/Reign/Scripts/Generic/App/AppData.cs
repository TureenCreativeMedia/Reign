using NaughtyAttributes;
using UnityEngine;

namespace Reign.ScriptableObjects.App
{
    [CreateAssetMenu(fileName = "AppData", menuName = "Reign/App Data")]
    public class AppData : ScriptableObject
    {
        [Header("Game")]
        [Label("Name")] public string string_AppName = "App";
        [Label("Version")] public string string_AppVersion = "Version 1.0.0";

        [Space(5)]

        [Header("Save System")]
        [Label("Save Password")] public string string_Password = "Reign_SAVEPASSWORD";
        [Label("Save Salt")] public string string_Salt = "Reign_SAVESALT";

        [Tooltip("File name saved to user/AppData/Company name/Product name/")]
        [Label("File Name")] public string string_SaveFileName = "save.REIGN";
        [Label("Save On Quit")] public bool bool_SaveOnQuit = true;
        [Label("Encrypt Saves")] public bool bool_EncryptSaves = true;
        
        [Tooltip("Note that the amount of iterations can be expensive on lower-end CPUs")]
        [ShowIf("bool_EncryptSaves")] [Label("Encryption Iterations")] [MinValue(1), MaxValue(100000)]public int int_EncryptionIterations = 25000;

        [Space(5)]

        [Header("Discord RPC")]
        [Label("Discord RPC Enabled")] public bool bool_DiscordRPC;
        [ShowIf("bool_DiscordRPC")] [Label("Discord App ID")] public long long_DiscordAppID;
    }
}
