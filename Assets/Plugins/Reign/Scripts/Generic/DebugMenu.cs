#pragma warning disable CS0162 // Unreachable code detected

using Reign.Systems;
using TMPro;
using UnityEngine;

namespace Reign.Generic
{
    public class DebugMenu : MonoBehaviour
    {
        [SerializeField] TMP_Text text;

        private void Start()
        {
            if (!Reign.currentGameCertificates.IS_DEBUG)
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            text.text = $@"{Reign.currentGameCertificates.GAME_NAME} v{Reign.currentGameCertificates.VERSION}<size=75%>
• Reign v{ReignServiceDetails.REIGN_VERSION}
• Discord Status: {DiscordSystem.Instance.IsConnected}";
        }
    }
}