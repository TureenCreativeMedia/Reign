using Reign.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Reign.Essentials
{
    [RequireComponent(typeof(BoxCollider))]
    public class BoxTrigger : MonoBehaviour
    {
        public BoxCollider colliderReference { get; private set; }
        public bool triggerEnabled = true;

        [SerializeField] string searchTag;
        [SerializeField] private UnityEvent onEnter;
        [SerializeField] private UnityEvent onExit;

        private void Awake()
        {
            colliderReference = GetComponent<BoxCollider>();

            if (colliderReference != null) colliderReference.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (triggerEnabled && (other.CompareTag(searchTag) || string.IsNullOrEmpty(searchTag)))
            {
                onEnter?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (triggerEnabled && (other.CompareTag(searchTag) || string.IsNullOrEmpty(searchTag)))
            {
                onExit?.Invoke();
            }
        }

        private void OnDestroy()
        {
            onExit?.Invoke();
        }

        public void EnableTrigger() => triggerEnabled = true;
        public void DisableTrigger() => triggerEnabled = false;
        public void ToggleTrigger() => triggerEnabled = !triggerEnabled;
    }
}
