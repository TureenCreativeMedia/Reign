using Reign.Generic;
using System;
using UnityEngine;

namespace Reign.Systems
{
    [Serializable]
    public struct ScreenSettings
    {
        public Vector2Int Vector2_ScreenResolution;
        public bool bool_Fullscreen;
        public bool bool_VSync;
    }

    public class ScreenSystem : Singleton<ScreenSystem>, IDataHandler
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
            int int_ScreenWidth = SETTING.Vector2_ScreenResolution.x <= 0 ? Screen.currentResolution.width : SETTING.Vector2_ScreenResolution.x;
            int int_ScreenHeight = SETTING.Vector2_ScreenResolution.y <= 0 ? Screen.currentResolution.height : SETTING.Vector2_ScreenResolution.y; ;

            Screen.SetResolution(int_ScreenWidth, int_ScreenHeight, SETTING.bool_Fullscreen);

            QualitySettings.vSyncCount = SETTING.bool_VSync ? 1 : 0;
        }
    }
}
