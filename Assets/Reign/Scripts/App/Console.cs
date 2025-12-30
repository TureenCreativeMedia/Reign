using NUnit.Framework;
using UnityEngine;

namespace reign
{
    public class Console : MonoBehaviour
    {
        [SerializeField] ReignWidgetGroup r_Widgets;

        private void Start()
        {
            if(App.u_localdata.console)
            {
                r_Widgets.CreateWidgets();
            }
        }
    }
}
