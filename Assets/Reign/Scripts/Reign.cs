using Reign.Generic;
using UnityEngine;
using Reign.Events;

namespace Reign.Main
{
    public struct OnProgramStarted : IEvent { }
    public struct OnProgramQuit : IEvent { }
    public class Reign : Singleton<Reign>
    {
        public const string string_REIGN_VERSION = "v0.5.0.1";

        private void Start()
        {
            Debug.Log($"<color=#4287F5><b>Reign {string_REIGN_VERSION} Started</b></color>");
            
            EventBus.Publish(new OnProgramStarted { });
        }
        private void OnApplicationQuit()
        {
            EventBus.Publish(new OnProgramQuit { });
        }
    }
}
