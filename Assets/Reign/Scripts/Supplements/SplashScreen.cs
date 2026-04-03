using System.Collections;
using NaughtyAttributes;
using Reign.Systems.Loading;
using UnityEditor.SearchService;
using UnityEngine;

namespace Reign.Supplements
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField, Label("Play Splash Screen")] bool bool_PlaySplashScreen;
        [SerializeField, Label("Animator")] Animator Animator_Splash;
        [SerializeField, Scene, Label("Scene To Load After Splash")] string string_SceneToLoad;

        void Awake()
        {
            // Only attempt to get the animator if we're going to be playing the splash screen
            if (bool_PlaySplashScreen)
                TryGetComponent(out Animator_Splash);
        }

        IEnumerator Start()
        {
            if (!bool_PlaySplashScreen || string.IsNullOrEmpty(string_SceneToLoad)) yield break;

            Animator_Splash.Play("Splash");
            yield return new WaitUntil(() => !Animator_Splash.GetCurrentAnimatorStateInfo(0).IsName("Splash"));
            StartCoroutine(LoadingSystem.Instance.LoadScene(string_SceneToLoad, 1, 2));
        }
    }
}
