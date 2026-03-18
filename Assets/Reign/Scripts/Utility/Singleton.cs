using UnityEngine;

namespace reign
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
                        GameObject obj = new GameObject(typeof(T).Name);
                        T_Instance = obj.AddComponent<T>();
                    }
                }

                return T_Instance;
            }
        }

        protected virtual void Awake()
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
        protected override void Awake()
        {
            base.Awake();

            if (T_Instance == this)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
