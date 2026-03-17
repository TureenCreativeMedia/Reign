using System;
using UnityEngine;
using UnityEngine.UI;

namespace reign
{
    [Serializable]
    [RequireComponent(typeof(Animator), typeof(Canvas), typeof(CanvasScaler))]
    public class Widget : Animated
    {
        Canvas Canvas_WidgetCanvas;
        public Color GetGraphicColor()
        {
            return Graphic_Graphic.color;
        }
        public virtual void ToggleCanvas(bool STATE)
        {
            Canvas_WidgetCanvas.gameObject.SetActive(STATE);
        }
        void Awake()
        {
            Canvas_WidgetCanvas = GetComponent<Canvas>();
        }
    }
}
