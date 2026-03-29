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
        public IEnumerator LoadScene(string SCENE, float WAIT = 1.0f, float DURATION = 1.0f)
        {
            Image_Screen.enabled = true;
            yield return StartCoroutine(ImageFader.FadeAlpha(Image_Screen, 1.0f, DURATION));

            AsyncOperation AsyncOperation_SceneLoad = SceneManager.LoadSceneAsync(SCENE);
            yield return new WaitUntil(() => AsyncOperation_SceneLoad.isDone);

            yield return StartCoroutine(ImageFader.FadeAlpha(Image_Screen, 0.01f, DURATION));
            Image_Screen.enabled = false;
        }
    }
}
