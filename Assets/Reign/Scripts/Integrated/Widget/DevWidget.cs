using TMPro;
using UnityEngine;

namespace reign
{
    public class DevWidget : ReignWidget
    {
        [SerializeField] bool b_EnabledByDefault;
        [SerializeField] TMP_Text tmp_Text;
        private void OnEnable()
        {
            Main.a_OnFrame += WidgetUpdate;
        }
        private void OnDisable()
        {
            Main.a_OnFrame -= WidgetUpdate;
        }
        private void Start()
        {
            tmp_Text.enabled = b_EnabledByDefault;
        }
        void WidgetUpdate()
        {
            if (InputSystem.GetInput("F12", InputSystem.KeyType.Down))
            {
                tmp_Text.enabled = !tmp_Text.enabled;
            }

            if (!tmp_Text.enabled) return;

            tmp_Text.text = $"<b>Profiler (Toggle F12)</b>\n{Main.Instance.Dump()}";
        }
    }
}
