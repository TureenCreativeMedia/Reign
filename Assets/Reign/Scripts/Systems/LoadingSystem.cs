using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

namespace reign
{
    public class LoadingSystem : BaseSystem
    {
        public static Action<string, float, float> Action_TryLoad;
        [SerializeField] Widget Widget_LoadingScreen;

        void OnEnable()
        {
            Widget_LoadingScreen.gameObject.SetActive(false);
            Action_TryLoad += LoadSceneAsyncVoid;
        }
        void OnDisable()
        {
            Action_TryLoad -= LoadSceneAsyncVoid;
        }

        void LoadSceneAsyncVoid(string NAME, float DELAY, float CROSSFADEDURATION)
        {
            StartCoroutine(LoadSceneAsync(NAME, DELAY, CROSSFADEDURATION));
        }
        public IEnumerator LoadSceneAsync(string NAME, float DELAY = 1.0f, float CROSSFADEDURATION = 0.4f)
        {
            Widget_LoadingScreen.gameObject.SetActive(true); 
            StartCoroutine(Widget_LoadingScreen.CrossfadeAlpha(1.0f, CROSSFADEDURATION));
            yield return new WaitUntil(()=>Widget_LoadingScreen.GetGraphicColor().a >= 0.99f);
            yield return new WaitForSecondsRealtime(DELAY);
            
            AsyncOperation AsyncOperation_SceneLoad = SceneManager.LoadSceneAsync(NAME);
            yield return new WaitUntil(()=> AsyncOperation_SceneLoad.isDone);
            StartCoroutine(Widget_LoadingScreen.CrossfadeAlpha(0.0f, CROSSFADEDURATION));
            yield return new WaitUntil(()=>Widget_LoadingScreen.GetGraphicColor().a <= 0.01f);
            Widget_LoadingScreen.gameObject.SetActive(false);
        }
    }
}
