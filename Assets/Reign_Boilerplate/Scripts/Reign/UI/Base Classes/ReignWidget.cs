using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace reign
{
    public class ReignWidget : MonoBehaviour
    {
        public static Action a_OnCreate;
        [HideInInspector] public RectTransform u_RectTransform;
        private void Start()
        {
            Initialise();
            a_OnCreate?.Invoke();
        }

        void Initialise()
        {
            u_RectTransform = gameObject.GetComponent<RectTransform>();
        }

        public virtual void SetWidgetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public float GetWidgetWidth()
        {
            return u_RectTransform.sizeDelta.x;
        }

        public float GetWidgetHeight()
        {
            return u_RectTransform.sizeDelta.y;
        }

        // Animated
        public virtual IEnumerator Reposition(Vector2 startPosition, Vector2 newPosition, float duration)
        {
            u_RectTransform.localPosition = startPosition;

            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / duration);
                Vector2 lerpedPosition = Vector2.Lerp(startPosition, newPosition, t);
                u_RectTransform.localPosition = lerpedPosition;
                yield return null;
            }

            // Ensure final position
            u_RectTransform.localPosition = newPosition;
        }
    }
}
