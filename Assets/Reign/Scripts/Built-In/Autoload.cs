using UnityEngine;

namespace reign
{
    public class Autoload : MonoBehaviour
    {
        [SerializeField] string string_SceneName;
        [SerializeField] bool bool_Immediate;
        void Start()
        {
            if (bool_Immediate)
            {
                Load();
            }
        }

        public void Load()
        {
            if(string.IsNullOrEmpty(string_SceneName)) return;

            LoadingSystem.Action_TryLoad?.Invoke(string_SceneName, 1, 1);
        }
    }
}
