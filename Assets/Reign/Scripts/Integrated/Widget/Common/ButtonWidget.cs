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

        [SerializeField] UnityEvent u_OnHover;
        [SerializeField] UnityEvent u_OnUnhover;

        private void Awake()
        {
            u_Button = GetComponent<Button>();
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            u_Button.OnPointerEnter(eventData);
            u_OnHover?.Invoke();
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            u_Button.OnPointerExit(eventData);
            u_OnUnhover?.Invoke();
        }
    }
}
