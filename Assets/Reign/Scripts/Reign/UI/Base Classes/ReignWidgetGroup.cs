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
        public List<GameObject> u_WidgetInstances;

        void Awake()
        {
            if (Canvas == null) Canvas = transform;
        }

        public virtual void VerifyConnectors()
        {
            r_Connectors.Clear();

            for (int i = 0; i < Canvas.childCount; ++i)
            {
                if (Canvas.GetChild(i).TryGetComponent<ReignWidget>(out var r_AttempedParse)) r_Connectors.Add(r_AttempedParse);
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
            GameObject u_Instanciated = Instantiate(widget, parent);

            u_WidgetInstances.Add(u_Instanciated);

            u_Instanciated.TryGetComponent<ReignWidget>(out var r_AttempedParse);
            
            r_Connectors.Add(r_AttempedParse);
            Debug.Log($"Created widget {widget.name} at {parent}");
        }

        public virtual void DestroyWidget(ReignWidget widget)
        {
            Destroy(widget);
        }

        public virtual void DestroyWidgetInstances()
        {
            foreach (var widget in u_WidgetInstances)
            {
                if (widget != null)
                {
                    Destroy(widget);
                }
            }

            r_Connectors.Clear();
            u_WidgetInstances.Clear();
        }
    }
}