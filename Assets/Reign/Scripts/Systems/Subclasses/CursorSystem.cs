using System.Collections;
using System.Collections.Generic;
using Reign.AssetLibrary;
using Reign.Generic;
using Reign.ScriptableObjects.Cursor;
using UnityEngine;

namespace Reign.Systems.Screen
{
    [RequireComponent(typeof(CursorAssetLibrary))]
    public class CursorSystem : Singleton<CursorSystem>
    {
        private CursorAssetLibrary CursorAssetLibrary_Cursors;
        [SerializeField] private CursorSetting CursorSetting_Default;

        private void Awake()
        {
            TryGetComponent(out CursorAssetLibrary_Cursors);
        }
        private void Start()
        {
            SetCursor(CursorSetting_Default.string_ID);
        }
        public static void SetCursor(string NAME)
        {
            // Return the Cursor Texture
            var CursorObject = Instance.CursorAssetLibrary_Cursors.GetAsset(NAME);
            UnityEngine.Cursor.SetCursor(CursorObject.Texture2D_Texture, CursorObject.Vector2_Hotspot, CursorMode.Auto);
        }
    }
}
