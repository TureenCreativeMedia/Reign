using System;
using UnityEngine;
using UnityEngine.UI;

namespace reign
{
    [Serializable]
    [RequireComponent(typeof(Animator), typeof(Canvas), typeof(CanvasScaler))]
    public class Widget : Animated
    {
        // Canvas_WidgetCanvas should be a child of a GameObject with this script attached.
        Canvas Canvas_WidgetCanvas;
        public Color GetGraphicColor()
        {
            return Graphic_Graphic.color;
        }
        public virtual void ToggleCanvas(bool STATE)
        {
            Canvas_WidgetCanvas.gameObject.SetActive(STATE);
        }
        public override void OriginAwake()
        {
            base.OriginAwake();
            Canvas_WidgetCanvas = GetComponent<Canvas>();
        }
    }
}
