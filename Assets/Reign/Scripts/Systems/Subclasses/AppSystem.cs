using NaughtyAttributes;
using Reign.Generic;
using Reign.ScriptableObjects.App;
using UnityEngine;

namespace Reign.Systems.App
{
    public class AppSystem : Singleton<AppSystem>
    {
        [Label("App Data")] public AppData AppData_Data;
    }
}
