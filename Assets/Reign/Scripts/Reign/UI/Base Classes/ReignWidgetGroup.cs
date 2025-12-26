using System.Collections.Generic;
using UnityEngine;

namespace reign
{
	public class ReignWidgetGroup : MonoBehaviour
	{
        // Widget groups should only be applied to Canvas objects

        [HideInInspector] Transform Canvas;
        public List<ReignWidget> r_Connectors;
        public List<GameObject> u_Widgets;

        void Awake()
        {
            Canvas = transform;
        }

        public virtual void VerifyConnectors()
        {
            r_Connectors.Clear();

            for (int i = 0; i < Canvas.childCount; ++i)
            {
                r_Connectors.Add(Canvas.GetChild(i).GetComponent<ReignWidget>());
            }
        }

        public virtual void CreateWidgets()
        {
            for (int i = 0; i < u_Widgets.Count; ++i)
            {
                CreateWidget(u_Widgets[i], Canvas);
            }
        }
        public virtual void CreateWidget(GameObject widget, Transform parent)
        {
            Instantiate(widget, parent);
            r_Connectors.Add(widget.GetComponent<ReignWidget>());
            Debug.Log($"Created widget at {parent}");
        }

        public virtual void DestroyWidget(ReignWidget widget)
        {
            Destroy(widget.gameObject);
        }
    }
}