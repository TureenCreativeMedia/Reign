using System;
using Unity.Profiling;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace reign
{
    [DefaultExecutionOrder(-100)]
    public class Main : MonoBehaviour
    {
        // *ONE* object with Main should always be present in the scene.

        ProfilerRecorder u_SystemUsedMemoryRecorder;

        public static Main Instance;

        public string s_RuntimeKey;

        public static Action a_OnFrame;
        public static Action<float> a_OnTimePassed;

        public static Action a_OnWake;
        public static Action a_OnHang;

        public float f_AppRuntime;
        public long l_AppStartUnixTimestamp;

        [SerializeField] ReignWidgetGroup AppWidget;
        [SerializeField] string s_StartScene;
        [SerializeField] TextAsset u_AppSettingsTextAsset;

        public float f_InternalClock = 0.0f;
        public float f_DeltaCount = 0.0f;
        public int i_RuntimeFrames = 0;

        private void OnEnable()
        {
            u_SystemUsedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
            a_OnFrame += AddFrame;
        }
        private void OnDisable()
        {
            u_SystemUsedMemoryRecorder.Dispose();
            a_OnFrame -= AddFrame;
        }
        void AddFrame() => i_RuntimeFrames++;

        private void Update()
        {
            if (InputSystem.GetInput("F11", InputSystem.KeyType.Down))
            {
                // Alternate fullscreen
                Screen.SetResolution(Screen.width, Screen.height, !Screen.fullScreen);
            }

            f_AppRuntime = Time.realtimeSinceStartup;
            f_InternalClock += Time.deltaTime;

            f_DeltaCount += Time.deltaTime; // Clock that resets every second
            a_OnTimePassed?.Invoke(f_DeltaCount); // Run every second

            if (f_DeltaCount >= 1.0f)
            {
                f_DeltaCount = 0.0f;
            }

            a_OnFrame?.Invoke();
        }

        private void Awake()
        {
            // Reset internal clock
            SetInternalClock(0.0f);

            // Set initial app unix timestamp
            l_AppStartUnixTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            
            // Get app preferences
            App.InitializeAppData(u_AppSettingsTextAsset);

            // Create runtime key
            s_RuntimeKey = Extensions.KeyGenerator.GenerateKey(16);
            Debug.Log($"Runtime key generated: {s_RuntimeKey}");

            // Create instance
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // For persistence between scenes
            }

            if (!string.IsNullOrEmpty(s_StartScene) && SceneManager.GetActiveScene().name != s_StartScene)
            {
                SceneManager.LoadScene(s_StartScene); // If start scene isn't null, leap to it ASAP.
            }

            UnityEditor.AssetDatabase.Refresh(); // Refresh asset database

            f_DeltaCount = 0; // Per-second timer reset

            a_OnWake?.Invoke(); // Waking: Happens at the end of the awake function of this script.

            AppWidget.CreateWidgets();
        }

        public void SetInternalClock(float time) => f_InternalClock = time;

        public void Hang()
        {
            // Hanging: quitting the app

            SaveSystem.Save();
            a_OnHang?.Invoke();
            Application.Quit();

#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
        }

        public string Dump()
        {
            return $"Main Dump\nSystem Used Memory: {u_SystemUsedMemoryRecorder.LastValue / 1048576}MB\nLifetime Frames: {i_RuntimeFrames}\nPer-Second Count: {f_DeltaCount}\nApp Runtime: {f_AppRuntime}\nUnix Start Timestamp: {l_AppStartUnixTimestamp}\nCurrent Scene: {SceneManager.GetActiveScene().name}\nUnique Runtime Key: {s_RuntimeKey}\nDiscord Rich Presence: {(DiscordController.b_Connected ? "Connected" : "Not Connected")}";
        }
    }
}
