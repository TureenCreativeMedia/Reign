using System.Collections.Generic;
using UnityEngine;

namespace reign
{
	public class ReignWidgetGroup : MonoBehaviour
	{
        // Widget groups should only be applied to Canvas objects

        [SerializeField] Transform Canvas;
        public List<ReignWidget> r_Connectors;
        public List<GameObject> u_Widgets;

        void Awake()
        {
            if (Canvas == null) Canvas = transform;
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
            if (u_Widgets.Count < 0) return;

            for (int i = 0; i < u_Widgets.Count; ++i)
            {
                CreateWidget(u_Widgets[i], Canvas);
            }
        }
        public virtual void CreateWidget(GameObject widget, Transform parent)
        {
            Instantiate(widget, parent);
            r_Connectors.Add(widget.GetComponent<ReignWidget>());
            Debug.Log($"Created widget {widget.name} at {parent}");
        }

        public virtual void DestroyWidget(ReignWidget widget)
        {
            Destroy(widget.gameObject);
        }   

        public virtual void DestroyWidgets()
        {
            r_Connectors.Clear();
            for (int i = 0; i < u_Widgets.Count; ++i)
            {
                Destroy(u_Widgets[i]);
                u_Widgets.RemoveAt(i);
            }
        }
    }
}