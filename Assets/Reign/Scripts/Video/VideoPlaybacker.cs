using UnityEngine;
using UnityEngine.Video;

namespace reign
{
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoPlaybacker : MonoBehaviour, IOriginAwake
    {
        protected VideoPlayer VideoPlayer_Player;
        [SerializeField] VideoClip VideoClip_Video;

        public void OriginAwake()
        {
            VideoPlayer_Player.renderMode = VideoRenderMode.CameraNearPlane;
            VideoPlayer_Player.targetCamera = MasterSystem.Instance.Camera_MasterCamera;
            VideoPlayer_Player.clip = VideoClip_Video;
        }
        public void QueueNewClip(VideoClip CLIP)
        {
            VideoPlayer_Player.clip = CLIP;
        }
        public void Play()
        {
            VideoPlayer_Player.Play();
        }
    }
}
