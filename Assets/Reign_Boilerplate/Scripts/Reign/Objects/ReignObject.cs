using System;
using UnityEngine;

namespace reign
{
    public class ReignObject : MonoBehaviour
    {
        public static Action a_OnChangeVisibility;

        Renderer u_ObjectRenderer;
        [SerializeField] bool b_PrecacheRenderer;

        private void OnEnable()
        {
            Main.a_OnTimePassed += ObjectUpdateTime; // Attach object update to time passed
            Main.a_OnFrame += ObjectUpdateFrame; // Attach frame update to on frame
        }

        private void OnDisable()
        {
            Main.a_OnTimePassed -= ObjectUpdateTime;
            Main.a_OnFrame += ObjectUpdateFrame;
        }

        private void Awake()
        {
            if (b_PrecacheRenderer)
            {
                u_ObjectRenderer = gameObject.GetComponent<Renderer>();
            }
        }

        public void SetObjectVisibility(bool visible)
        {
            if (u_ObjectRenderer != null)
            {
                u_ObjectRenderer.enabled = visible;
            }
            else
            {
                gameObject.GetComponent<Renderer>().enabled = visible;
            }
            a_OnChangeVisibility?.Invoke();
        }

        public enum SocketType { Set, Remove }
        public void SetSocket(GameObject gameObject, SocketType socketType = SocketType.Set, bool stopPhysics = true)
        {
            if(stopPhysics)
            {
                var rb = gameObject.GetComponent<Rigidbody>();

                if (rb != null) rb.isKinematic = (socketType == SocketType.Set);
            }

            if (socketType == SocketType.Set)
            {
                this.gameObject.transform.SetParent(gameObject.transform);
                this.gameObject.transform.position = gameObject.transform.position;
            }
            else
            {
                this.gameObject.transform.SetParent(null); // Return to scene hierarchy
            }
        }

        public virtual void ObjectUpdateTime(float delta)
        {

        }

        public virtual void ObjectUpdateFrame()
        {

        }
    }
}
