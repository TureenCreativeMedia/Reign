using System;
using UnityEngine;

namespace reign
{

    /// <summary>
    /// Base class for all systems
    /// </summary>
    [Serializable]
    public class BaseSystem : MonoBehaviour
    {
        
    }

    /// <summary>
    /// Parent system to subsystems
    /// </summary>
    public class MasterSystem : PersistentSingleton<MasterSystem>
    {
        [Header("References")]
        public Camera Camera_MasterCamera;

        [Header("Systems References")]

        public OriginSystem _OriginSystem;
        public TimeSystem _TimeSystem;
        public SoundSystem _SoundSystem;
        public PostProcessingSystem _PostProcessingSystem;
        public SaveSystem _SaveSystem;
        public InputSystem _InputSystem;
        public DiscordSystem _DiscordSystem;
        public ScreenshotSystem _ScreenshotSystem;
        public CursorSystem _CursorSystem;
        public LoadingSystem _LoadingSystem;

        void Start() => Initialise();
        void Initialise()
        {
            Camera_MasterCamera = GetComponentInChildren<Camera>();
            _OriginSystem = GetComponentInChildren<OriginSystem>();
            _TimeSystem = GetComponentInChildren<TimeSystem>();
            _SoundSystem = GetComponentInChildren<SoundSystem>();
            _PostProcessingSystem = GetComponentInChildren<PostProcessingSystem>();
            _SaveSystem = GetComponentInChildren<SaveSystem>();
            _InputSystem = GetComponentInChildren<InputSystem>();
            _DiscordSystem = GetComponentInChildren<DiscordSystem>();
            _ScreenshotSystem = GetComponentInChildren<ScreenshotSystem>();
            _CursorSystem = GetComponentInChildren<CursorSystem>();
            _LoadingSystem = GetComponentInChildren<LoadingSystem>();
        }
    }
}
