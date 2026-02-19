using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace reign
{
    public class PostProcessingSystem : BaseSystem, IDataHandler
    {
        protected PostProcessData PostProcessData_Cached;
        bool bool_PostProcessingEnabled;
        public static System.Action Action_AttemptRefreshPostProcessing;
        void OnEnable()
        {
            Action_AttemptRefreshPostProcessing += Initialize;
        }
        void OnDisable()
        {
            Action_AttemptRefreshPostProcessing -= Initialize;
        }
        void Initialize()
        {
            if(!bool_PostProcessingEnabled) return;

            Volume _Volume = MasterSystem.Instance.Camera_MasterCamera.GetComponent<Volume>();

            _Volume.enabled = bool_PostProcessingEnabled;

            _Volume.profile.TryGet<Bloom>(out Bloom bloom);
            _Volume.profile.TryGet<Vignette>(out Vignette vignette);
            _Volume.profile.TryGet<ChromaticAberration>(out ChromaticAberration chromaticAberration);
            _Volume.profile.TryGet<LensDistortion>(out LensDistortion lensDistortion);
            _Volume.profile.TryGet<FilmGrain>(out FilmGrain filmGrain);
            _Volume.profile.TryGet<MotionBlur>(out MotionBlur motionBlur);

            bloom.intensity.value = PostProcessData_Cached.float_BloomIntensity;
            vignette.intensity.value = PostProcessData_Cached.float_VignetteIntensity;
            chromaticAberration.intensity.value = PostProcessData_Cached.float_ChromaticAberrationIntensity;
            lensDistortion.intensity.value = PostProcessData_Cached.float_LensDistortionIntensity;
            filmGrain.intensity.value = PostProcessData_Cached.float_FilmGrainIntensity;
            motionBlur.intensity.value = PostProcessData_Cached.float_MotionBlurIntensity;
        }

        public void LoadData(GameData DATA)
        {
            PostProcessData_Cached = DATA.PostProcessData_PostProcessData;
            bool_PostProcessingEnabled = PostProcessData_Cached.bool_PostProcessingEnabled;
            Action_AttemptRefreshPostProcessing?.Invoke();
        }

        public void SaveData(ref GameData DATA)
        {
        }
    }
}
