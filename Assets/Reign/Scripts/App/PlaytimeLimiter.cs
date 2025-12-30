using System;
using UnityEngine;

namespace reign
{
    public class PlaytimeLimiter : MonoBehaviour
    {
        public static Action a_OnPlaytimeWarn;
        public static Action<float> a_SetTimer;
        [SerializeField] ReignWidgetGroup r_LimiterWidget;

        public static bool b_CanCreate;
        public float f_Timer = 0.0f;
        public const float f_WarningTime = 10800f;
        
        private void OnEnable()
        {
            a_SetTimer += SetTimer;

            Main.a_OnFrame += CheckLimitPlaytime;
            a_OnPlaytimeWarn += r_LimiterWidget.CreateWidgets;
        }
        private void OnDisable()
        {
            a_SetTimer -= SetTimer;

            Main.a_OnFrame -= CheckLimitPlaytime;
            a_OnPlaytimeWarn -= r_LimiterWidget.CreateWidgets;
        }

        void SetTimer(float time)
        {
            f_Timer = time;
        }
        private void Awake()
        {
            b_CanCreate = true;
        }
        void CheckLimitPlaytime()
        {
            if (!b_CanCreate)
            {
                f_Timer = 0.0f;
                return;
            }

            f_Timer += Time.deltaTime;
            if(f_Timer >= f_WarningTime)
            {
                a_OnPlaytimeWarn?.Invoke();
            }
        }
    }
}
