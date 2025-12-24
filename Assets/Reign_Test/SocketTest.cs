using UnityEngine;

namespace reign
{
    public class SocketTest : MonoBehaviour
    {
        [SerializeField] ReignObject u_Obj;
        [SerializeField] GameObject u_SocketObject;

        private void PickUp()
        {
            u_Obj.SetSocket(u_SocketObject, ReignObject.SocketType.Set, true);
        }

        private void OnEnable()
        {
            Main.a_OnFrame += ScriptUpdate;
        }

        private void OnDisable()
        {
            Main.a_OnFrame -= ScriptUpdate;
        }

        void ScriptUpdate()
        {
            if (u_Obj != null) return;

            if(InputSystem.GetInput("E", InputSystem.KeyType.Down))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                    u_Obj = hit.collider.gameObject.GetComponent<ReignObject>();

                    if(u_Obj != null) PickUp();
                }
            }
        }
    }
}
