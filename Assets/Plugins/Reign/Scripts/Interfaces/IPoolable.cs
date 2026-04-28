using Reign.Generics;

namespace Reign.Interfaces
{
    public interface IPoolable
    {
        void OnSpawn();
        void OnReclaim();
        void OnReset();
    }
}
