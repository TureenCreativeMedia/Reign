using TMPro;
using UnityEngine;

namespace reign
{
    public class Widget_FPS : Widget
    {
        [SerializeField] TMP_Text TMP_Text_FPS;

        void OnEnable()
        {
            EventBus.Subscribe<OnSecondEvent>(UpdateFPS);
        }
        void OnDisable()
        {
            EventBus.Unsubscribe<OnSecondEvent>(UpdateFPS);
        }
        void UpdateFPS(OnSecondEvent EVENT)
        {
            TMP_Text_FPS.text = $"FPS: {(int)(1.0f / Time.float_UnscaledDeltaTime)}";
        }
    }
}
