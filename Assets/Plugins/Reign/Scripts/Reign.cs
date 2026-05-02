using Reign.Systems;
using Reign.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Reign
{
    public sealed class Reign : Singleton<Reign>
    {
        [SerializeField] private List<SystemBase> requiredSystems;
        [SerializeField] private bool log;

        private void Awake()
        {
            if (!log) return;

            if (IsUsable())
            {
                Debug.Log($"<color=#008ec2ff><b>Started Reign v{ReignServiceDetails.REIGN_VERSION}</b></color>");
            }
            else
            {
                Debug.LogError($"Reign encountered errors when starting.");
            }
        }

        private bool IsUsable()
        {
            // Leave space for other requirements later

            return HasRequiredSystems();
        }

        /// <summary>
        /// Does the script have any required systems? If so, are they present?
        /// </summary>
        /// <returns>bool</returns>
        public bool HasRequiredSystems()
        {
            bool success = !requiredSystems.Any() || requiredSystems.All(system => system != null);

            if (success && log) Debug.Log("<color=#008ec2ff><b>Reign</b></color> found all required systems!");
            return success;
        }
    }
}
