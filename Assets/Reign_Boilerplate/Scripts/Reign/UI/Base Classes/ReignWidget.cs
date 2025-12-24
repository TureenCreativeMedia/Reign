using UnityEngine;

namespace reign
{
    public class ReignWidget : MonoBehaviour
    {
        public virtual void SetWidgetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public virtual void Reposition(Vector2 screenPosition)
        {
            gameObject.GetComponent<RectTransform>().position = screenPosition;
        }
    }
}
