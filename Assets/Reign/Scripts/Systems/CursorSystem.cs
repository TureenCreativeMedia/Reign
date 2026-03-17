using UnityEngine;

namespace reign
{
    [RequireComponent(typeof(CursorAssetLibrary))]
    public class CursorSystem : BaseSystem
    {
        private CursorAssetLibrary CursorAssetLibrary_Cursors;

        private void Awake()
        {
            CursorAssetLibrary_Cursors = GetComponent<CursorAssetLibrary>();
        }
        private void Start()
        {
            SetCursor("Arrow", CursorLockMode.None, true);
        }
        public void SetCursor(string ID, CursorLockMode MODE, bool VISIBLE = true)
        {
            CursorSetting CursorType_Get = GetCursor(ID);

            Cursor.SetCursor(CursorType_Get.Texture2D_CursorTexture, CursorType_Get.Vector2_Hotspot, CursorMode.Auto);
            
            Cursor.lockState = MODE;
            Cursor.visible = VISIBLE;
        }
        public CursorSetting GetCursor(string ID)
        {
            CursorSetting CursorSetting_Attempt = CursorAssetLibrary_Cursors.GetAsset(ID);

            if (CursorSetting_Attempt != null)
            {
                return CursorSetting_Attempt;
            }
            else
            {
                Logger.Instance.Log(Logger.enum_LogIntensity.Warning, $"Could not change Cursor texture");
                return null;
            }
        }
    }
}
