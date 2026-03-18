using System.Collections;
using UnityEngine;

namespace reign
{
    /// <summary>
    /// Contains the actions for Awake, Start and Update methods
    /// </summary>
    public class OriginSystem : BaseSystem
    {
        // Awake, Start and LateStart are all called once as the systems are always present,
        // to call Awake or Start inside one script, please use Unity's Awake and Start.

        // For Update and OnSecond, please use the OriginSystem (this)
        public static System.Action Action_OnAwake;
        public static System.Action Action_OnStart;
        public static System.Action Action_OnLateStart;
        public static System.Action Action_OnSecond;
        public static System.Action Action_OnUpdate;
        public static System.Action Action_OnGameQuit;

        private void Awake()
        {
            Action_OnAwake?.Invoke();
        }
        private void Start()
        {
            Action_OnStart?.Invoke();

            StartCoroutine(LateStart());
            StartCoroutine(SyncedTimer());
        }
        private void Update()
        {
            Action_OnUpdate?.Invoke();
        }
        private IEnumerator LateStart()
        {
            yield return null;
            Action_OnLateStart?.Invoke();
        }
        private IEnumerator SyncedTimer()
        {
            while (true)
            {
                Action_OnSecond?.Invoke();
                yield return new WaitForSeconds(1.0f);
            }
        }
        private void OnApplicationQuit()
        {
            Action_OnGameQuit?.Invoke();
        }
    }
}