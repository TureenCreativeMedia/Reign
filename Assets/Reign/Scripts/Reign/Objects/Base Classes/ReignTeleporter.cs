using System.Collections;
using UnityEngine;

namespace reign
{
    [RequireComponent(typeof(BoxCollider))]
    public class ReignTeleporter : MonoBehaviour
    {
        [SerializeField] Vector3 v_TeleportPosition;
        [SerializeField] string s_OnlyWithTag;

        private void OnTriggerEnter(Collider other)
        {
            if (!string.IsNullOrEmpty(s_OnlyWithTag))
            {
                if (!other.CompareTag(s_OnlyWithTag))
                {
                    return;
                }
            }

            StartCoroutine(Teleport(other.gameObject));
        }

        public virtual IEnumerator Teleport(GameObject other)
        {
            var u_ScriptCall = other.GetComponents<MonoBehaviour>(); // Get relevant scripts

            Extensions.UnityAdditions.ToggleScriptArray(u_ScriptCall, false); // Stop updating

            other.transform.position = v_TeleportPosition; // Teleport

            yield return new WaitForSeconds(0.02f); // Wait shortly

            Extensions.UnityAdditions.ToggleScriptArray(u_ScriptCall, true); // Re-enable
        } 
    }
}
