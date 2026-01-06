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
        private void OnEnable()
        {
            Main.a_OnFrame += UpdateWidget;
            tmp_Text = GetComponent<TMP_Text>();
        }
        private void OnDisable()
        {
            Main.a_OnFrame -= UpdateWidget;
        }
        void UpdateWidget()
        {
            tmp_Text.text = $"FPS: {(int)(1.0f / Time.unscaledDeltaTime)}";
        }
    }
}
