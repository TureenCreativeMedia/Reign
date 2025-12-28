using TMPro;
using UnityEngine;

namespace reign
{
    public class DevWidget : ReignWidget
    {
        [SerializeField] bool b_EnabledByDefault;
        [SerializeField] TMP_Text u_Text;
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
            u_Text.enabled = b_EnabledByDefault;
        }
        void WidgetUpdate()
        {
            if (InputSystem.GetInput("F12", InputSystem.KeyType.Down))
            {
                u_Text.enabled = !u_Text.enabled;
            }

            if (!u_Text.enabled) return;

            u_Text.text = $"<b>Profiler (Toggle F12)</b>\n{Main.Instance.Dump()}";
        }
    }
}
