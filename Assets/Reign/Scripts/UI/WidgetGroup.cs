using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace reign
{
    public class WidgetGroup : MonoBehaviour, IDestroyableTransform
    {
        public List<Widget> List_GroupWidgets {get; private set;} = new();
        
        public Transform Transform_Parent;
        public List<Transform> Transform_Children;
        public void DestroyTransform(Transform TRANSFORM)
        {
            if (!Transform_Children.Contains(TRANSFORM)) return;
            
            Transform_Children.Remove(TRANSFORM);
            DestroyTransform(TRANSFORM);
        }

        public void CreateChild(GameObject PREFAB)
        {
            GameObject GameObject_Child = Instantiate(PREFAB);
            Transform_Children.Add(GameObject_Child.transform);
        }
        public void DestroyWidgets(List<Widget> MATCH)
        {
            foreach (Widget widget in List_GroupWidgets)
            {
                if (MATCH.Contains(widget))
                {
                    List_GroupWidgets.Remove(widget);
                    Destroy(widget);
                }
            }
        }
    }
}
