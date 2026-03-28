using NaughtyAttributes;
using Reign.Generic;
using Reign.ScriptableObjects.App;

namespace Reign.Systems
{
    public class AppSystem : Singleton<AppSystem>
    {
        [Label("App Data")] public AppData AppData_Data;
    }
}
