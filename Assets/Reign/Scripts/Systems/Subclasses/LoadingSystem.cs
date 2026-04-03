using NaughtyAttributes;
using Reign.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Reign.Systems.Loading
{
    public class LoadingSystem : Singleton<LoadingSystem>
    {
        public bool bool_isLoading;
        [SerializeField, Label("Image")] Image Image_Screen;

        private void OnEnable()
        {
            SceneManager.activeSceneChanged += OnNewScene;
        }
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnNewScene;
        }

        void OnNewScene(Scene ARG0, Scene ARG1)
        {
            StartCoroutine(FadeOut());
        }

        public IEnumerator FadeOut(float DURATION = 2.0f)
        {
            StartCoroutine(ColorLerper.FadeAlpha(Image_Screen, 0.0f, DURATION));
            yield return new WaitUntil(() => Image_Screen.color.a <= 0.01f);
            Image_Screen.enabled = false;
            bool_isLoading = false;
        }
        public IEnumerator LoadScene(string SCENE, float WAIT = 1.0f, float DURATION = 2.0f)
        {
            Color Color_StartColor = Image_Screen.color;
            Image_Screen.enabled = true;

            if (Color_StartColor.a != 0.0f)
            {
                Image_Screen.color = new Color(Color_StartColor.r, Color_StartColor.g, Color_StartColor.b, 0.0f);
            }

            bool_isLoading = true;
            yield return new WaitForSecondsRealtime(WAIT);

            StartCoroutine(ColorLerper.FadeAlpha(Image_Screen, 1.0f, DURATION));
            yield return new WaitUntil(() => Image_Screen.color.a >= 0.99f);

            yield return new WaitForSecondsRealtime(WAIT);

            AsyncOperation AsyncOperation_SceneLoad = SceneManager.LoadSceneAsync(SCENE);
            yield return new WaitUntil(() => AsyncOperation_SceneLoad.isDone);
        }
    }
}
