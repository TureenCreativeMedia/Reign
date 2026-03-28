using Reign.Generic;
using Reign.Systems;
using System.Collections.Generic;
using UnityEngine;
using Reign.Events;
using Reign.Backend;
using NaughtyAttributes;
using System.Linq;

namespace Reign.Main
{
    public struct OnProgramStarted : IEvent { }
    public class Reign : Singleton<Reign>
    {
        public const string string_REIGN_VERSION = "v0.5.0.0";

        private void Start()
        {
            EventBus.Publish(new OnProgramStarted { });
            Debug.Log($"<color=#4287F5><b>Reign {string_REIGN_VERSION} Started</b></color>");
        }
    }
}
