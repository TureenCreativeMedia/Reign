using TMPro;
using UnityEngine;

namespace reign
{
    public class AppWidget : ReignWidget
    {
        [SerializeField] TMP_Text tmp_Text;
        string s_Text;
        private void Start()
        {
            s_Text = $"v{App.u_localdata.versionname} {(App.u_localdata.developmentbuild ? " - Development Build" : " ")}";
        }
        private void OnEnable()
        {
            Main.a_OnFrame += WidgetUpdate;
        }
        private void OnDisable()
        {
            Main.a_OnFrame -= WidgetUpdate;
        }
        void WidgetUpdate()
        {
            tmp_Text.text = $"{s_Text}";
        }
    }
}
