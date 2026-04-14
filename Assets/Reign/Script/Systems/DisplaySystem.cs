using UnityEngine;

namespace Reign.Systems
{
    public sealed class DisplaySystem : System<DisplaySystem>
    {
        private void Start()
        {
            // Eventually load from Save Data
            Screen.SetResolution(Screen.width, Screen.height, true);
        }

        private void ToggleFullscreen()
        {
            Screen.fullScreen = !Screen.fullScreen;
            Debug.Log($"Updated fullscreen: {Screen.fullScreen}");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F11))
            {
                ToggleFullscreen();
            }
        }
    }
}