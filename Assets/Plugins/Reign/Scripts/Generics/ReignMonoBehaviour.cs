using System.Collections.Generic;
using UnityEngine;

namespace Reign.Generics
{
    public class ReignMonoBehaviour : MonoBehaviour
    {
        public bool TrackActiveTime { get; private set; } = false;

        /// <summary>
        /// Get the current active time only if the object is tracking it
        /// </summary>
        public float ActiveTime
        {
            get
            {
                if (!TrackActiveTime)
                {
                    Debug.LogWarning("Unable to parse active time.");
                    return 0f;
                }

                return _activeTime;
            }
        }

        private float _activeTime = 0.0f;

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

        void Update()
        {
            // Only update if we're actually tracking
            if (TrackActiveTime)
            {
                _activeTime += Time.unscaledDeltaTime;
            }
        }
    }
}

