using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace reign
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(string s_SceneName)
        {
            SceneManager.LoadScene(s_SceneName);
        }

        public IEnumerator LoadSceneDelayed(string s_SceneName, float f_DelayTime)
        {
            yield return new WaitForSecondsRealtime(f_DelayTime);
            SceneManager.LoadScene(s_SceneName);
        }
    }
}
