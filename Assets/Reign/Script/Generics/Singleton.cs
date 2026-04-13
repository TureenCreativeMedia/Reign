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
                if (instance == null)
                {
                    // Find T if null (Singleton should only have one instance)

                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        // If this instance STILL doesn't exist, try our
                        // best to recreate it

                        GameObject recreate = new(typeof(T).Name);
                        instance = recreate.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        void Awake()
        {
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

            if (instance == null)
            {
                instance = this as T;
            }
            else if (instance != this)
            {
                // If the instance already exists but isn't us:
                // delete ourselves and prioritise the other

                Destroy(gameObject);
            }
        }
    }
}
