using System;
using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    [Serializable]
    public class Timer
    {
        public Action Action_OnTimerEnd;
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
            Action_OnTimerEnd?.Invoke();
        }

        public void Tick()
        {
            if (!bool_IsRunning) return;
            
            float_CurrentTime += MasterSystem.Instance._TimeSystem.float_TimeScale * UnityEngine.Time.deltaTime;
            if (float_CurrentTime >= float_Duration)
            {
                EndTimer();
            }
        }
    }
    public class TimeSystem : BaseSystem
    {
        public List<Timer> List_TimerQueue = new();
        public float float_TimeScale { get; private set; } = 1.0f;
        void OnEnable() 
        {
            OriginSystem.Action_OnUpdate += TickAll;
        }
        void OnDisable() 
        {
            OriginSystem.Action_OnUpdate -= TickAll;
        }
        void TickAll()
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
            _Timer.Action_OnTimerEnd += () => DestroyTimer(_Timer);
            return _Timer;
        }
        public void DestroyTimer(Timer TIMER)
        {
            List_TimerQueue.Remove(TIMER);
        }
        public void SetTimeScale(float NEWSCALE)
        {
            float_TimeScale = NEWSCALE;
            UnityEngine.Time.timeScale = float_TimeScale;
        }
    }
}
