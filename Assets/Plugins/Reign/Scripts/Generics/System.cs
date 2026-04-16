using Reign.Generics;

namespace Reign.Systems
{
    public abstract class System<T> : Singleton<T> where T : ReignMonoBehaviour
    {
    }
}