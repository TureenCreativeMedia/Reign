using UnityEngine;

namespace reign
{
    public class UITest : MonoBehaviour
    {
        [SerializeField] ReignWidget widget;
        void Start()
        {
            StartCoroutine(widget.Reposition(new(Screen.width + widget.GetWidgetWidth(), 0), 
                new(Screen.width / 2, 0), 0.4f));
        }
    }
}
