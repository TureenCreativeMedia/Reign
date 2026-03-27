using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reign.ScriptableObjects.App
{
    [CreateAssetMenu(fileName = "AppData", menuName = "Reign/App Data")]
    public class AppData : ScriptableObject
    {
        [Header("Game")]
        public string string_AppName = "App";
        public string string_AppVersion = "Version 1.0.0";

        [Space(5)]

        [Header("Save System")]
        public string string_Password = "Reign_SAVEPASSWORD";
        public string string_Salt = "Reign_SAVESALT";
        public string string_SaveFileName = "save.REIGN";
        public bool bool_EncryptSaves = true;
        public bool bool_SaveOnQuit = true;
    }
}
