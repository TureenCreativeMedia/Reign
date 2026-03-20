using System.Collections;
using UnityEngine;

namespace reign
{
    public struct OnSecondEvent : IEvent { public float TimePassed; }
    public struct OnAwakeEvent : IEvent { }
    public struct OnStartEvent : IEvent { }

    public class OriginSystem : BaseSystem
    {
        void Awake()
        {
            EventBus.Publish(new OnAwakeEvent { });
            StartCoroutine(SyncedTimer());
        }

        void Start()
        {
            EventBus.Publish(new OnStartEvent { });
        }

        private IEnumerator SyncedTimer()
        {
            while (true)
            {
                EventBus.Publish(new OnSecondEvent { TimePassed = 1f });
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}