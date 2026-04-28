using Reign.Generics;
using Reign.Interfaces;

namespace Reign.Essentials
{
    public class ReignObject : ReignMonoBehaviour, IPoolable
    {
        public virtual void OnReclaim()
        {
        }

        public virtual void OnReset()
        {
        }

        public virtual void OnSpawn()
        {
        }
    }
}