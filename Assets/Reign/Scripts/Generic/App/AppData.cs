using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reign.ScriptableObjects.App
{
    [CreateAssetMenu(fileName = "AppData", menuName = "Reign/App Data")]
    public class AppData : ScriptableObject
    {
        public string string_AppName = "App";
        public string string_AppVersion = "Version 1.0.0";
    }
}
