#pragma warning disable CS0162 // Unreachable code detected

using Reign.Systems;
using TMPro;
using UnityEngine;

namespace Reign.Generics
{
    public class DebugMenu : MonoBehaviour
    {
        [SerializeField] TMP_Text text;

        private void Start()
        {
            if (!GameCertificates.IS_DEBUG)
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
            text.text = $@"{GameCertificates.GAME_NAME} v{GameCertificates.VERSION}<size=75%>
• Reign v{ReignServiceDetails.REIGN_VERSION}
• Discord Status: {DiscordSystem.Instance.isConnected}";
        }
    }
}