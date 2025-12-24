using System;
using UnityEngine;

namespace reign
{
    [DefaultExecutionOrder(-100)]
    public class Main : MonoBehaviour
    {
        // *ONE* object with Main should always be present in the scene.

        public static Main Instance;

        public string s_RuntimeKey;

        public static Action a_OnFrame;
        public static Action<float> a_OnTimePassed;

        public static Action a_OnWake;
        public static Action a_OnHang;

        [SerializeField] string s_StartScene;
        [SerializeField] TextAsset u_AppSettingsTextAsset;

        float f_DeltaCount = 0.0f;
        private void Update()
        {
            f_DeltaCount += Time.deltaTime;
            a_OnTimePassed?.Invoke(f_DeltaCount);

            if (f_DeltaCount > 1.0f)
            {
                f_DeltaCount = 0.0f;
            }

            if (InputSystem.GetInput("F11", InputSystem.KeyType.Down))
            {
                Screen.SetResolution(Screen.width, Screen.height, !Screen.fullScreen);
            }

            a_OnFrame?.Invoke();
        }

        private void Awake()
        {
            App.InitializeAppData(u_AppSettingsTextAsset);

            // Instancing
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            DontDestroyOnLoad(gameObject); // Persisting

            a_OnWake?.Invoke(); // Waking: will happen once

            if (!string.IsNullOrEmpty(s_StartScene) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != s_StartScene)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(s_StartScene); // If start scene isn't null, leap to on runtime.
            }
        }

        public void SetDelta(float time) => f_DeltaCount = time;

        public void Hang()
        {
            // Hanging: quitting the app

            a_OnHang?.Invoke();
            Application.Quit();
        }
    }
}
