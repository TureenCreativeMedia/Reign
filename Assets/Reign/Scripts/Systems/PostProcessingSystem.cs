using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace reign
{
    public class PostProcessingSystem : BaseSystem, IDataHandler
    {
        Bloom BLOOM;
        Vignette VIGNETTE;
        ChromaticAberration CHROMATICABERRATION;
        LensDistortion LENSDISTORTION;
        FilmGrain FILMGRAIN;
        MotionBlur MOTIONBLUR;

        private protected PostProcessData PostProcessData_Cached;
        private protected VolumeProfile VolumeProfile_Cached;
        private protected bool bool_PostProcessingEnabled;
        void TryRefresh(Camera CAMERA)
        {
            if (CAMERA == null) return;

            if (!CAMERA.TryGetComponent(out Volume VOLUME)) return;

            if (VOLUME.profile == null) return;
            VOLUME.enabled = bool_PostProcessingEnabled;

            if (!bool_PostProcessingEnabled) return;

            if (VolumeProfile_Cached != VOLUME.profile)
            {
                VolumeProfile_Cached = VOLUME.profile;

                BLOOM = null;
                VIGNETTE = null;
                CHROMATICABERRATION = null;
                LENSDISTORTION = null;
                FILMGRAIN = null;
                MOTIONBLUR = null;
            }

            if (BLOOM == null) VOLUME.profile.TryGet(out BLOOM);
            if (VIGNETTE == null) VOLUME.profile.TryGet(out VIGNETTE);
            if (CHROMATICABERRATION == null) VOLUME.profile.TryGet(out CHROMATICABERRATION);
            if (LENSDISTORTION == null) VOLUME.profile.TryGet(out LENSDISTORTION);
            if (FILMGRAIN == null) VOLUME.profile.TryGet(out FILMGRAIN);
            if (MOTIONBLUR == null) VOLUME.profile.TryGet(out MOTIONBLUR);

            if (BLOOM != null)
                BLOOM.intensity.value = PostProcessData_Cached.float_BloomIntensity;

            if (VIGNETTE != null) VIGNETTE.intensity.value = PostProcessData_Cached.float_VignetteIntensity;

            if (CHROMATICABERRATION != null) CHROMATICABERRATION.intensity.value = PostProcessData_Cached.float_ChromaticAberrationIntensity;

            if (LENSDISTORTION != null) LENSDISTORTION.intensity.value = PostProcessData_Cached.float_LensDistortionIntensity;

            if (FILMGRAIN != null) FILMGRAIN.intensity.value = PostProcessData_Cached.float_FilmGrainIntensity;

            if (MOTIONBLUR != null) MOTIONBLUR.intensity.value = PostProcessData_Cached.float_MotionBlurIntensity;
        }

        public void LoadData(GameData DATA)
        {
            PostProcessData_Cached = DATA.PostProcessData_PostProcessData;
            bool_PostProcessingEnabled = PostProcessData_Cached.bool_PostProcessingEnabled;
            
            if (Camera.main != null) 
            {
                TryRefresh(Camera.main);
            }
        }

        public void SaveData(ref GameData DATA)
        {
        }
    }
}
