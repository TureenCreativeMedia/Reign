using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace reign
{
    [RequireComponent(typeof(Button))]
    public class ButtonWidget : ReignWidget
    {
        [SerializeField] Button u_Button;

        [SerializeField] UnityEvent u_OnClick;
        [SerializeField] UnityEvent u_OnHover;

        private void Awake()
        {
            u_Button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            u_Button.onClick = (Button.ButtonClickedEvent)u_OnClick;
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            u_Button.OnPointerEnter(eventData);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            u_Button.OnPointerExit(eventData);
        }
    }
}
