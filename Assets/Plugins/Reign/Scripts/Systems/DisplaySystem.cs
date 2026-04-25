using Reign.Interfaces;
using UnityEngine;

namespace Reign.Systems
{
    public sealed class DisplaySystem : System<DisplaySystem>, IDataHandler
    {
        public void LoadData(GameData DATA)
        {
            QualitySettings.vSyncCount = DATA.screenResolution.vsync ? 1 : 0;
            Screen.SetResolution(DATA.screenResolution.width, DATA.screenResolution.height, DATA.screenResolution.fullscreen);
        }

        public void SaveData(ref GameData DATA)
        {
        }

        private void Update()
        {
            // Fullscreen toggle
            if (Input.GetKeyDown(KeyCode.F11))
            {
                Screen.fullScreen = !Screen.fullScreen;
            }
        }
    }
}