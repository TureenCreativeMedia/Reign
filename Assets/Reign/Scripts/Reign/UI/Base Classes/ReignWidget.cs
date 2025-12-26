using System;
using System.Collections;
using UnityEngine;

namespace reign
{
    public class ReignWidget : MonoBehaviour
    {
        public static Action a_OnCreate;
        [HideInInspector] public RectTransform u_RectTransform;
        private void Start()
        {
            a_OnCreate?.Invoke();
            u_RectTransform = gameObject.GetComponent<RectTransform>();
            StartCoroutine(Rescale(new(1, 1), new(1, 1), 0));
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

        // Transforms
        public virtual IEnumerator Reposition(Vector2 startPosition, Vector2 newPosition, float duration)
        {
            u_RectTransform.localPosition = startPosition;

            float time = 0.0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / duration);
                Vector2 lerpedPosition = Vector2.Lerp(startPosition, newPosition, t);
                u_RectTransform.localPosition = lerpedPosition;
                yield return null;
            }

            // Ensure final position.
            u_RectTransform.localPosition = newPosition;
        }

        public virtual IEnumerator Rescale(Vector2 startScale, Vector2 newScale, float duration)
        {
            // Scaling entire widget.

            u_RectTransform.localScale = startScale;

            float time = 0.0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / duration);
                Vector2 lerpedScale = Vector2.Lerp(startScale, newScale, t);
                u_RectTransform.localScale = lerpedScale;
                yield return null;
            }

            u_RectTransform.localScale = newScale;
        }

        public virtual IEnumerator Resize(Vector2 startSize, Vector2 newSize, float duration)
        {
            // Sizing width and height of the widget rect transform.

            u_RectTransform.sizeDelta = startSize;

            float time = 0.0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / duration);
                Vector2 lerpedSize = Vector2.Lerp(startSize, newSize, t);
                u_RectTransform.sizeDelta = lerpedSize;
                yield return null;
            }

            u_RectTransform.sizeDelta = newSize;
        }
    }
}