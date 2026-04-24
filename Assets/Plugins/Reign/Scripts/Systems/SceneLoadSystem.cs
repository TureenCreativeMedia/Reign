using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace Reign.Systems
{
    public class SceneLoadSystem : System<SceneLoadSystem>
    {
        [SerializeField] Image loadOverlay;

        private void Start()
        {
            loadOverlay.color = new Color(0, 0, 0, 0);
        }

        /// <summary>
        /// Transition the load overlay image in or out
        /// </summary>
        /// <param name="isIn">Should the image fade in</param>
        /// <param name="speed">Speed of the fade</param>
        /// <returns></returns>
        private async Task TransitionAsync(bool isIn, float speed = 1.0f)
        {
            float target = isIn ? 1f : 0f;

            Color color = loadOverlay.color;

            while (!Mathf.Approximately(color.a, target))
            {
                await Task.Yield();

                color = loadOverlay.color;

                float newAlpha = Mathf.MoveTowards(color.a, target, Time.deltaTime * speed);

                loadOverlay.color = new Color(color.r, color.g, color.b, newAlpha);
            }
        }

        /// <summary>
        /// Load scene by name asynchronously
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Task<bool> LoadSceneAsync(string name)
        {
            var completionSource = new TaskCompletionSource<bool>();

            SceneManager.LoadSceneAsync(name).completed += _ =>
            {
                completionSource.SetResult(true);
            };

            return completionSource.Task;
        }

        /// <summary>
        /// Transition and await asynchronous scene load
        /// </summary>
        /// <param name="name">Name of the scene to load</param>
        /// <param name="transitionSpeed">Load overlay fade transition speed</param>
        /// <returns></returns>
        public async Task LoadScene(string name, float transitionSpeed = 1.0f)
        {
            // Fade in
            await TransitionAsync(true, transitionSpeed);

            // Await load
            await LoadSceneAsync(name);

            // Fade out
            await TransitionAsync(false, transitionSpeed);
        }
    }
}
