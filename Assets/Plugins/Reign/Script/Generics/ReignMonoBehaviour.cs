using System.Collections.Generic;
using UnityEngine;

namespace Reign.Generics
{
    public class ReignMonoBehaviour : MonoBehaviour
    {
        // Read Only because external sources shouldn't be able to alter this
        public IReadOnlyList<Transform> GetChildren
        {
            get
            {
                List<Transform> children = new();

                // First check if we have any to avoid a baseless for loop
                if (transform.childCount > 0)
                {
                    for (int i = 0; i < transform.childCount; ++i)
                    {
                        children.Add(transform.GetChild(i));
                    }
                }

                return children;
            }
        }
    }
}