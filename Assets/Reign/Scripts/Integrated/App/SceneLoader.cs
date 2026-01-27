using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace reign
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] string s_AutoTransition;
        [SerializeField] bool b_AutoIsLoadingScreen = true;

        private void Start()
        {
            TryLoadAuto();
        }

        void TryLoadAuto()
        {
            if (!string.IsNullOrEmpty(s_AutoTransition))
            {
                if ((b_AutoIsLoadingScreen))
                {
                    LoadSceneLoadingScreen(s_AutoTransition);
                }
                else
                {
                    LoadScene(s_AutoTransition);
                }
            }   
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public IEnumerator LoadSceneDelayed(string sceneName, float delayTime)
        {
            yield return new WaitForSecondsRealtime(delayTime);
            SceneManager.LoadScene(sceneName);
        }

        public void LoadSceneLoadingScreen(string sceneName)
        {
            LoadingScreenWidget.a_AttemptLoad?.Invoke(new(){ s_SceneName = sceneName, f_LoadDelay = 1.0f, b_ReleaseMouse = false});
        }
    }
}
