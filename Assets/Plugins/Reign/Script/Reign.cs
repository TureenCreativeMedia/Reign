using Reign.Systems;
using Reign.Generics;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Reign
{
    public sealed class Reign : Singleton<Reign>
    {
        [SerializeField] private List<System<ReignMonoBehaviour>> requiredSystems;

        private void Awake()
        {
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

        public bool HasRequiredSystems()
        {
            if (!requiredSystems.Any())
            {
                return true;
            }

            foreach (System<ReignMonoBehaviour> system in requiredSystems)
            {
                if (system == null)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
