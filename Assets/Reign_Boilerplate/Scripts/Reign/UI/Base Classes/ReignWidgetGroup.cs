using System.Collections.Generic;
using UnityEngine;

namespace reign
{
	public class ReignWidgetGroup : MonoBehaviour
	{
        [SerializeField] List<ReignWidget> r_Connectors;

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