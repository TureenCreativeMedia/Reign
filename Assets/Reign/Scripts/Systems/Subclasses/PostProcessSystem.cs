using NaughtyAttributes;
using Reign.Generic;
using Reign.Systems.Save;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Reign.Systems.Rendering
{
    [RequireComponent(typeof(GenericAssetLibrary))]
    public class PostProcessSystem : Singleton<PostProcessSystem>, IDataHandler
    {
        [Label("Library")] [SerializeField] GenericAssetLibrary GenericAssetLibrary_PostProcessAssetLibrary;
        [Label("Current Volume Profile")] public VolumeProfile PostProcessProfile_CurrentProfile;

        void Awake()
        {
            GenericAssetLibrary_PostProcessAssetLibrary = GetComponent<GenericAssetLibrary>();
        }
        public void LoadData(GameData DATA)
        {
            PostProcessProfile_CurrentProfile = (VolumeProfile)GenericAssetLibrary_PostProcessAssetLibrary.GetAsset(DATA.PostProcessProfile_ID);
        }

        public void SaveData(ref GameData DATA)
        {
            DATA.PostProcessProfile_ID = PostProcessProfile_CurrentProfile.name;
        }
    }
}
