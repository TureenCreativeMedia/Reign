using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace reign
{
    public struct TimerEndEvent : IEvent { public Timer Timer_Reference; }
    public struct TimerTickEvent : IEvent { public Timer Timer_Reference; }

    [Serializable]
    public class Timer
    {
        public float float_Duration = 0.0f;
        public float float_CurrentTime { get; private set; } = 0.0f;
        public bool bool_IsRunning = false;
        public Timer(float DURATION, bool AUTOSTART = false)
        {
            float_Duration = DURATION;

            if (AUTOSTART)
            {
                StartTimer();
            }
            else
            {
                bool_IsRunning = false;
                float_CurrentTime = 0.0f;
            }
        }

        public void PauseTimer() => bool_IsRunning = false;
        public void ResumeTimer() => bool_IsRunning = true;

        public void StartTimer()
        {
            bool_IsRunning = true;
            float_CurrentTime = 0.0f;
        }

        public void EndTimer()
        {
            bool_IsRunning = false;
            float_CurrentTime = 0.0f;
            EventBus.Publish(new TimerEndEvent { Timer_Reference = this });
        }

        public void Tick()
        {
            if (!bool_IsRunning) return;

            EventBus.Publish(new TimerTickEvent { Timer_Reference = this });
            float_CurrentTime += Time.float_ReignDeltaTime;

            if(float_Duration <= 0)
            {
                // Run until timer is ended manually.
                return;                
            }
            if (float_CurrentTime >= float_Duration)
            {
                EndTimer();
            }
        }
    }
    public class TimeSystem : BaseSystem, IUpdatable
    {
        public List<Timer> List_TimerQueue = new();
        void OnEnable() 
        {
            UpdateSystem.Register(this);
        }
        void OnDisable() 
        {
            UpdateSystem.Unregister(this);
        }
        void IUpdatable.Tick(float DELTATIME)
        {
            for (int i = List_TimerQueue.Count - 1; i >= 0; i--)
            {
                Timer Timer_Cache = List_TimerQueue[i];
                Timer_Cache.Tick();
            }
        }
        public Timer CreateTimer(float DURATION, bool AUTOSTART = false)
        {
            Timer _Timer = new(DURATION, AUTOSTART);
            List_TimerQueue.Add(_Timer);
            EventBus.Subscribe<TimerEndEvent>(TimerEndDestroy);
            return _Timer;
        }
        void TimerEndDestroy(TimerEndEvent EVENT)
        {
            DestroyTimer(EVENT.Timer_Reference);
            EventBus.Unsubscribe<TimerEndEvent>(TimerEndDestroy);
        }
        public void DestroyTimer(Timer TIMER)
        {
            List_TimerQueue.Remove(TIMER);
        }
    }
}
