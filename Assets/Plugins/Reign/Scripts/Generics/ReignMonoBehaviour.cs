using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

namespace Reign.Generics
{
    public class ReignMonoBehaviour : MonoBehaviour
    {
        public bool TrackAliveTime { get; private set; } = false;

        public float AliveTime
        {
            get
            {
                if (!TrackAliveTime)
                {
                    Debug.LogWarning("Alive time is not tracked: enable TrackAliveTime.");
                    return 0f;
                }

                return _aliveTime;
            }
        }

        private float _aliveTime = 0.0f;

        // IEnumerable to avoid bad heap allocation
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
            if (TrackAliveTime)
            {
                _aliveTime += Time.unscaledDeltaTime;
            }
        }
    }
}