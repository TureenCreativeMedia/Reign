using UnityEngine;

namespace Reign.Systems
{
    public class DisplaySystem : System<DisplaySystem>
    {
        void Start()
        {
            // Eventually load from Save Data
            Screen.SetResolution(Screen.width, Screen.height, true);
        }
    }
}