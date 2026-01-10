using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace reign
{
    [RequireComponent(typeof(TMP_Text))]
    public class FPSWidget : MonoBehaviour
    {
        TMP_Text tmp_Text;
        const float f_UpdateTime = 0.2f;
        float f_Elapsed = 0.0f;
        private void OnEnable()
        {
            Main.a_OnDelta += UpdateWidget;
            tmp_Text = GetComponent<TMP_Text>();
        }
        private void OnDisable()
        {
            Main.a_OnDelta -= UpdateWidget;
        }
        void UpdateWidget(float t)
        {
            f_Elapsed += t;
            if (f_Elapsed > f_UpdateTime)
            {
                tmp_Text.text = $"FPS: {(int)(1.0f / Time.unscaledDeltaTime)}";
                f_Elapsed = 0.0f;
            }
        }
    }
}
