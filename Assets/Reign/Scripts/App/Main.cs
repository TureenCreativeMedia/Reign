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

        public string s_RuntimeKey { get; private set; }
        public string s_RenderingAPI;

        public static Action a_OnFrame;
        public static Action a_OnSecondPassed;
        public static Action<float> a_OnDelta;

        public static Action a_OnWake;
        public static Action a_OnInitialised;
        public static Action a_OnHang;

        public float f_AppRuntime;
        public long l_AppStartUnixTimestamp;

        [SerializeField] ReignWidgetGroup AppWidget;
        [SerializeField] TextAsset u_AppSettingsTextAsset;

        public float f_TimeScale = 1.0f;
        public float f_DeltaCount = 0.0f;
        public float f_DeltaTime => f_DeltaCount * f_TimeScale;
        public float f_SecondCount = 0.0f;
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
        private void Awake()
        {
            a_OnWake?.Invoke();
        }
        private void Update()
        {
            f_AppRuntime = Time.realtimeSinceStartup;

            f_DeltaCount = Time.unscaledDeltaTime;

            f_SecondCount += Time.deltaTime;

            if (f_SecondCount >= 1.0f)
            {
                a_OnSecondPassed?.Invoke(); // Run every second
                f_SecondCount = 0.0f;
            }

            a_OnFrame?.Invoke();

            a_OnDelta?.Invoke(f_DeltaCount);
        }

        private void Start()
        {
            // Set time scale
            SetTimeScale(1.0f);

            // Find rendering API
            s_RenderingAPI = SystemInfo.graphicsDeviceVersion;

            // Set initial app unix timestamp
            l_AppStartUnixTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            
            // Get app preferences
            App.InitialiseAppData(u_AppSettingsTextAsset);

            // Create runtime key
            s_RuntimeKey = Extensions.KeyGenerator.GenerateKey(16);

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

#if UNITY_EDITOR
            AssetDatabase.Refresh(); // Refresh asset database
#endif
            f_DeltaCount = 0;

            if (App.u_localdata.appwidgets)
            {
                AppWidget.CreateWidgets();
            }

            a_OnInitialised?.Invoke();
        }
        public void SetTimeScale(float scale) => f_TimeScale = scale;
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
            return $"Rendering API: {s_RenderingAPI}\nSystem Used Memory: {u_SystemUsedMemoryRecorder.LastValue / 1048576}MB\nLifetime Frames: {i_RuntimeFrames}\nDelta: {f_DeltaCount}\nApp Runtime: {f_AppRuntime}\nUnix Start Timestamp: {l_AppStartUnixTimestamp}\nCurrent Scene: {SceneManager.GetActiveScene().name}\nUnique Runtime Key: {s_RuntimeKey}\nDiscord Rich Presence: {(DiscordController.b_Connected ? "Connected" : "Not Connected")}";
        }
    }
}
