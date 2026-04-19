using System.Collections;
using System.Linq;
using UnityEngine;

namespace Reign.Generics
{
    [DefaultExecutionOrder(-1)]
    public class Singleton<T> : ReignMonoBehaviour where T : ReignMonoBehaviour
    {
        [SerializeField] bool dontDestroyOnLoad;

        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance != null) return instance;

                if (!Application.isPlaying) return null;

                instance = FindObjectOfType<T>();
                return instance;
            }
        }

        IEnumerator Start()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                yield break;
            }

            instance = this as T;

            if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }
    }
}
