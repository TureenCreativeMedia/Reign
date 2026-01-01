using TMPro;
using UnityEngine;

namespace reign
{
    public class AppWidget : ReignWidget
    {
        [SerializeField] TMP_Text tmp_Text;
        void Start()
        {
            tmp_Text.text = $"Reign {App.u_localdata.versionname} {(App.u_localdata.developmentbuild ? " - Development Build" : " ")}";
        }
    }
}
