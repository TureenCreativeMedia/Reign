using UnityEngine;

namespace reign
{
    public class Audio : MonoBehaviour
    {
        // Used for the Reign 8 channel audio system.

        public static Audio Instance;
        public MasterChannel[] r_Channels;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
