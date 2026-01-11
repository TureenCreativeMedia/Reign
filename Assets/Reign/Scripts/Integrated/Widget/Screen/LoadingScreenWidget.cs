using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace reign
{
    public class LoadingData
    {
        public string s_SceneName;
        public float f_LoadDelay;
        public bool b_ReleaseMouse;
    }
    public class LoadingScreenWidget : MonoBehaviour
    {
        public static Action<LoadingData> a_AttemptLoad;
        public static Action a_OnBeginLoad;
        public static Action a_OnEndLoad;

        [SerializeField] GameObject u_Container;
        [SerializeField] Slider u_Slider;
        [SerializeField] TMP_Text tmp_Text;

        private void Awake()
        {
            if (u_Container == null)
            {
                u_Container = transform.GetChild(0).gameObject;
            }

            a_AttemptLoad += Load;
        }

        private void OnDestroy()
        {
            a_AttemptLoad -= Load;
        }

        private void Start()
        {
            u_Container.SetActive(false);
        }

        public void Load(LoadingData loadingData)
        {
            StartCoroutine(LoadAsync(loadingData));
        }

        private IEnumerator LoadAsync(LoadingData loadingData)
        {
            if (loadingData == null || !Extensions.SceneManagement.SceneExists(loadingData.s_SceneName))
            {
                yield break;
            }

            u_Container.SetActive(true);

            float f_SavedTimeScale = Main.Instance.f_TimeScale;
            Main.Instance.f_TimeScale = 0;

            if (loadingData.b_ReleaseMouse)
            {
                Cursor.Instance.SetCursor("Arrow", new() { b_Visible = true, u_LockMode = CursorLockMode.None});
            }

            if (loadingData.f_LoadDelay > 0.0f)
            {
                yield return new WaitForSecondsRealtime(loadingData.f_LoadDelay);
            }

            a_OnBeginLoad?.Invoke();

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(loadingData.s_SceneName);

            while (!asyncOperation.isDone)
            {
                if (u_Slider != null)
                {
                    u_Slider.value = asyncOperation.progress;
                    tmp_Text.text = $"{(int)(u_Slider.value*100)}%";
                }
                yield return null;
            }

            a_OnEndLoad?.Invoke();
            Main.Instance.f_TimeScale = f_SavedTimeScale;
            u_Container.SetActive(false);
        }
    }
}
