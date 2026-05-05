using System.Collections.Generic;
using UnityEngine;

namespace Reign.Generic
{
    public class ReignMonoBehaviour : MonoBehaviour
    {
        public readonly bool TrackInternalTime = false;
        public float InternalTimer {get; private set;} = 0.0f;

        private void Update()
        {
            if (!TrackInternalTime) return;
            InternalTimer += Time.unscaledDeltaTime;
        }

        /// <summary>
        /// Get children of this ReignMonoBehaviour
        /// </summary>
        public IEnumerable<Transform> GetChildren
        {
            get
            {
                for (int i = 0; i < transform.childCount; ++i)
                {
                    yield return transform.GetChild(i);
                }
            }
        }
    }
}

