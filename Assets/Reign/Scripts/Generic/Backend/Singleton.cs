using UnityEngine;

namespace Reign.Generic
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T T_Instance;
        public static T Instance
        {
            get
            {
                if (T_Instance == null)
                {
                    T_Instance = FindObjectOfType<T>();

                    if (T_Instance == null)
                    {
                        GameObject GameObject_Object = new GameObject(typeof(T).Name);
                        T_Instance = GameObject_Object.AddComponent<T>();
                    }
                }

                return T_Instance;
            }
        }
        public virtual void Awake()
        {
            if (T_Instance == null)
            {
                T_Instance = this as T;
            }
            else if (T_Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        public override void Awake()
        {
            base.Awake();

            if (T_Instance == this)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
