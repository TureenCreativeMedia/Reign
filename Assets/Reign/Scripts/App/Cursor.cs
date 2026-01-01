using System;
using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    public class CursorSetting
    {
        public CursorLockMode u_LockMode = CursorLockMode.None;
        public bool B_Visible = true;
    }

    [Serializable]
    public class CursorType
    {
        public Texture2D u_Texture;
        public string s_Name;
        public Vector2 v_Hotspot;
    }

    public class Cursor : MonoBehaviour
    {
        public List<CursorType> l_CursorTypes;

        private void Start()
        {
            SetCursor("Arrow", new()
            {
                u_LockMode = CursorLockMode.None,
                B_Visible = true,
            }
            );
        }
        public Texture2D GetCursorTexture(string cursorName)
        {
            for (int i = 0; i < l_CursorTypes.Count; ++i)
            {
                if (l_CursorTypes[i].s_Name == cursorName)
                {
                    return l_CursorTypes[i].u_Texture;
                }
            }

            return null;
        }

        public Vector2 GetCursorHotspot(string cursorName)
        {
            for (int i = 0; i < l_CursorTypes.Count; ++i)
            {
                if (l_CursorTypes[i].s_Name == cursorName)
                {
                    return l_CursorTypes[i].v_Hotspot;
                }
            }

            return Vector2.zero;
        }

        public void SetCursor(string cursorName, CursorSetting cursorSetting)
        {
            UnityEngine.Cursor.SetCursor(GetCursorTexture(cursorName), GetCursorHotspot(cursorName), CursorMode.ForceSoftware);
            UnityEngine.Cursor.lockState = cursorSetting.u_LockMode;
            UnityEngine.Cursor.visible = cursorSetting.B_Visible;
        }
    }
}
