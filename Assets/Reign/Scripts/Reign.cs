using Reign.Generic;
using Reign.Systems;
using System.Collections.Generic;
using UnityEngine;
using Reign.Events;

namespace Reign.Main
{
    public struct OnProgramStarted : IEvent { }
    public class Reign : PersistentSingleton<Reign>
    {
        public const string string_REIGN_VERSION = "v0.5.0.0";
        public List<BaseSystem> List_Systems;

        private void Start()
        {
            EventBus.Publish(new OnProgramStarted { });
            Debug.Log($"<color=#4287F5><b>Reign {string_REIGN_VERSION} Started</b></color>");
        }
        public BaseSystem GetSystem(string NAME)
        {
            foreach (BaseSystem SYSTEM in List_Systems)
            {
                if (SYSTEM.string_SysName == NAME)
                {
                    return SYSTEM;
                }
            }
            Debug.LogWarning($"Could not find System with name '{NAME}'");
            return null;
        }
    }
}
