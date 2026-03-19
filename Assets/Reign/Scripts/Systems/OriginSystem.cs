using System.Collections;
using UnityEngine;

namespace reign
{
    public struct OnSecondEvent : IEvent { public float TimePassed; }
    public struct OnAwakeEvent : IEvent { }
    public struct OnGameQuitEvent : IEvent { }

    public class OriginSystem : BaseSystem
    {
        void Awake()
        {
            EventBus.Publish(new OnAwakeEvent { });
            StartCoroutine(SyncedTimer());
        }
        private IEnumerator SyncedTimer()
        {
            while (true)
            {
                EventBus.Publish(new OnSecondEvent { TimePassed = 1f });
                yield return new WaitForSeconds(1.0f);
            }
        }
        private void OnApplicationQuit()
        {
            EventBus.Publish(new OnGameQuitEvent { });
        }
    }
}