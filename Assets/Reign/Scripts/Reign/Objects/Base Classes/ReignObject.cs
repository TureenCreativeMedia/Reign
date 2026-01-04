using System;
using UnityEngine;

namespace reign
{
    public class ReignObject : MonoBehaviour
    {
        public static Action a_OnChangeVisibility;

        Renderer u_ObjectRenderer;
        Transform u_OriginParent;
        
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

            u_OriginParent = gameObject.transform.parent;
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
            if (stopPhysics)
            {
                if (gameObject.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.isKinematic = (socketType == SocketType.Set);
                }
            }

            if (socketType == SocketType.Set)
            {
                gameObject.transform.SetParent(gameObject.transform);
                gameObject.transform.position = gameObject.transform.position;
            }
            else
            {
                gameObject.transform.SetParent(u_OriginParent); // Return to original parent
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
