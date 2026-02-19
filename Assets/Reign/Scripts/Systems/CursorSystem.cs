using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    [Serializable]
    public class CursorType
    {
        public string string_ID;
        public Texture2D Texture2D_CursorTexture;
        public Vector2 Vector2_Hotspot;
    }
    public class CursorSystem : BaseSystem
    {
        [SerializeField] List<CursorType> CursorType_Cursors = new();

        void OnEnable()
        {
            OriginSystem.Action_OnAwake += () => SetCursor("Arrow", CursorLockMode.None, true);
        }
        void OnDisable()
        {
            OriginSystem.Action_OnAwake -= () => SetCursor("Arrow", CursorLockMode.None, true);
        }
        public void SetCursor(string ID, CursorLockMode LOCKMODE, bool VISIBLE = true)
        {
            CursorType CursorType_Get = GetCursor(ID);
            Cursor.SetCursor(CursorType_Get.Texture2D_CursorTexture, CursorType_Get.Vector2_Hotspot, CursorMode.Auto);
            Cursor.lockState = LOCKMODE;
            Cursor.visible = VISIBLE;
        }
        public CursorType GetCursor(string ID)
        {
            foreach (CursorType CURSOR in CursorType_Cursors)
            {
                if(CURSOR.string_ID == ID)
                {
                    return CURSOR;
                }
            }
            return null;
        }
    }
}
