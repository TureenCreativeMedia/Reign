using Reign.Generic;
using Reign.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace Reign
{
    public class Reign : PersistentSingleton<Reign>
    {
        public List<BaseSystem> List_Systems;

        public BaseSystem GetSystem<T>(BaseSystem BaseSystem)
        {
            foreach (BaseSystem AbstractBaseSystem_System in List_Systems)
            {
                if (AbstractBaseSystem_System == BaseSystem)
                {
                    return AbstractBaseSystem_System;
                }
            }

            Debug.LogWarning($"System '{BaseSystem}' was not available in {List_Systems}");
            return null;
        }
    }
}
