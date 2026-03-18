using TMPro;
using UnityEngine;

namespace reign
{
    public class Widget_FPS : Widget
    {
        [SerializeField] TMP_Text TMP_Text_FPS;

        void OnEnable()
        {
            OriginSystem.Action_OnSecond += UpdateFPS;
        }
        void OnDisable()
        {
            OriginSystem.Action_OnSecond -= UpdateFPS;
        }
        void UpdateFPS()
        {
            TMP_Text_FPS.text = $"FPS: {(int)(1.0f / Time.float_UnscaledDeltaTime)}";
        }
    }
}
