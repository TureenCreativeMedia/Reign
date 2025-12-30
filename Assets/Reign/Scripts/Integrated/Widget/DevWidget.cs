using System;
using TMPro;
using UnityEngine;

namespace reign
{
    public class DevWidget : ReignWidget
    {
        public static Action a_ToggleProfiler;
        [SerializeField] bool b_EnabledByDefault;
        [SerializeField] TMP_Text tmp_Text;
        private void OnEnable()
        {
            a_ToggleProfiler += ToggleProfiler;
            Main.a_OnFrame += WidgetUpdate;
        }
        private void OnDisable()
        {
            a_ToggleProfiler -= ToggleProfiler;
            Main.a_OnFrame -= WidgetUpdate;
        }
        private void Start()
        {
            tmp_Text.enabled = b_EnabledByDefault;
        }

        void ToggleProfiler()
        {
            tmp_Text.enabled = !tmp_Text.enabled;
        }
        void WidgetUpdate()
        {
            if (!tmp_Text.enabled) return;

            tmp_Text.text = $"<b>Profiler</b>\n{Main.Instance.Dump()}";
        }
    }
}
