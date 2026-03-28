using Reign.Generic;
using Reign.Systems.Audio;
using Reign.Systems.Save;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Reign.Systems.Screen
{
    [Serializable]
    public struct ScreenSettings
    {
        public Vector2Int Vector2_ScreenResolution;
        public AntialiasingMode AntialiasingMode_Antialiasing;
        public bool bool_Fullscreen;
        public bool bool_VSync;
    }

    public class ScreenSystem : Singleton<ScreenSystem>
    {
        public void LoadData(GameData DATA)
        {
            SetScreenSettings(DATA.ScreenSettings_Data);
        }

        public void SaveData(ref GameData DATA)
        {
        }

        public void SetScreenSettings(ScreenSettings SETTING)
        {
            int int_ScreenWidth = SETTING.Vector2_ScreenResolution.x <= 0 ? UnityEngine.Screen.currentResolution.width : SETTING.Vector2_ScreenResolution.x;
            int int_ScreenHeight = SETTING.Vector2_ScreenResolution.y <= 0 ? UnityEngine.Screen.currentResolution.height : SETTING.Vector2_ScreenResolution.y; ;

            UnityEngine.Screen.SetResolution(int_ScreenWidth, int_ScreenHeight, SETTING.bool_Fullscreen);

            QualitySettings.vSyncCount = SETTING.bool_VSync ? 1 : 0;
        }
    }
}
