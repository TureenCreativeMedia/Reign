using System.Collections.Generic;

namespace reign
{
    public class UpdateSystem : BaseSystem
    {
        public static readonly List<IUpdatable> List_Updatables = new();

        public static void Register(IUpdatable EVENT)
        {
            List_Updatables.Add(EVENT);
        }

        public static void Unregister(IUpdatable EVENT)
        {
            List_Updatables.Remove(EVENT);
        }

        void Update()
        {
            for (int i = 0; i < List_Updatables.Count; ++i)
            {
                List_Updatables[i].Tick(Time.float_ReignDeltaTime);
            }
        }
    }
}