using Reign.Systems.Rendering;
using Reign.Systems.Save;
using UnityEngine;
using UnityEngine.Rendering;

namespace Reign.Supplements
{
    [RequireComponent(typeof(Camera), typeof(Volume))]
    public class PostProcessingCamera : MonoBehaviour
    {
        private Volume Volume_PPV;
        private void Awake()
        {
            Volume_PPV = GetComponent<Volume>();
            Volume_PPV.profile = PostProcessSystem.Instance.PostProcessProfile_CurrentProfile;
        }

    }
}
