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
        [SerializeField] [Label("Image")] Image Image_Screen;
        public IEnumerator LoadScene(string SCENE, float WAIT = 1.0f, float DURATION = 2.0f)
        {
            Color Color_StartColor = Image_Screen.color;
            Image_Screen.enabled = true;
            
            if (Color_StartColor.a != 0.0f)
            {
                Image_Screen.color = new Color(Color_StartColor.r, Color_StartColor.g, Color_StartColor.b, 0.0f);
            }

            yield return StartCoroutine(ImageFader.FadeAlpha(Image_Screen, 1.0f, DURATION));

            AsyncOperation AsyncOperation_SceneLoad = SceneManager.LoadSceneAsync(SCENE);
            AsyncOperation_SceneLoad.allowSceneActivation = false;

            while (AsyncOperation_SceneLoad.progress < 0.9f) yield return null;

            Debug.Log($"LoadingSystem: Scene '{SCENE}' loaded.");
            yield return new WaitForSecondsRealtime(WAIT);

            StartCoroutine(ImageFader.FadeAlpha(Image_Screen, 0.0f, DURATION));
            AsyncOperation_SceneLoad.allowSceneActivation = true;

            yield return new WaitUntil(() => Image_Screen.color.a <= 0.01f);
            Image_Screen.enabled = false;
        }
    }
}
