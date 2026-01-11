using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    public class GameObjectPool : MonoBehaviour
    {
        public static GameObjectPool Instance;
        [SerializeField] List<GameObject> u_Pool;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        public void PullList(List<GameObject> gameObject)
        {
            for (int i = 0; i < gameObject.Count; i++)
            {
                u_Pool.Add(gameObject[i]);
            }
        }
        public void PushBack(GameObject gameObject)
        {
            u_Pool.Add(gameObject);
            Destroy(gameObject);
        }

        public void PopForward(GameObject gameObject, Transform parent)
        {
            Instantiate(gameObject, parent);
            u_Pool.Remove(gameObject);
        }
    }
}
