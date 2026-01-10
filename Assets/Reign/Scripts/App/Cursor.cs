using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace reign
{
    public class CursorSetting
    {
        public CursorLockMode u_LockMode;
        public bool b_Visible;
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
        public string s_CurrentName;
        public static Cursor Instance;
        public List<CursorType> l_CursorTypes;

        private void Awake()
        {
            SetCursor("Arrow", new() { b_Visible = true, u_LockMode = CursorLockMode.None });

            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
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

        public void Unlock(string cursorName)
        {
            SetCursor(cursorName, new(){ b_Visible = true, u_LockMode = CursorLockMode.None });
        }
        public void SetCursor(string cursorName, CursorSetting cursorSetting)
        {
            if (!string.IsNullOrEmpty(cursorName))
            {
                UnityEngine.Cursor.SetCursor(GetCursorTexture(cursorName), GetCursorHotspot(cursorName), 0);
                s_CurrentName = cursorName;
            }

            UnityEngine.Cursor.lockState = cursorSetting.u_LockMode;
            UnityEngine.Cursor.visible = cursorSetting.b_Visible;
        }
    }
}
