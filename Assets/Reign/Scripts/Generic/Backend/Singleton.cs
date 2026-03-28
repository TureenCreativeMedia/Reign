using NaughtyAttributes;
using UnityEngine;

namespace Reign.Generic
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] [Label("Don't Destroy On Load")] bool bool_DontDestroyOnLoad; 
        protected static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject GameObject_Object = new GameObject(typeof(T).Name);
                        instance = GameObject_Object.AddComponent<T>();
                    }
                }

                return instance;
            }
        }
        private void Awake()
        {
            if (bool_DontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

            if (instance == null)
            {
                instance = this as T;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
