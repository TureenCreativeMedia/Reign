using System.Collections.Generic;
using UnityEngine;

namespace reign
{
	public class ReignWidgetGroup : MonoBehaviour
	{
        // Widget groups should only be applied to Canvas objects

        [HideInInspector] Transform Canvas;
        [SerializeField] List<ReignWidget> r_Connectors;
        
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
        public virtual void CreateWidget(ReignWidget widget, Transform parent)
        {
            Instantiate(widget, parent);
            r_Connectors.Add(widget);
            Debug.Log($"Created {widget.GetType()} {widget} at {parent}");
        }

        public virtual void DestroyWidget(ReignWidget widget)
        {
            Destroy(widget.gameObject);
        }
    }
}