using System.Linq;
using UnityEngine;

namespace reign
{
    [RequireComponent(typeof(AudioSource))]
    public class MasterChannel : MonoBehaviour
    {
        [SerializeField] bool b_ChannelActive = true;
        [SerializeField] AudioSource u_Channel;
        [SerializeField] AudioPool r_Pool;

        private void Awake()
        {
            if (u_Channel == null)
            {
                u_Channel = GetComponent<AudioSource>();
            }
            else
            {
                b_ChannelActive = true;
            }
        }
        public void PlaySound(string soundName, float volume = 0.7f)
        {
            if (!b_ChannelActive) return;

            for (int j = 0; j < r_Pool.u_AudioClips.Count; ++j)
            {
                if (r_Pool.u_AudioClips[j].name == soundName)
                {
                    u_Channel.PlayOneShot(r_Pool.u_AudioClips[j], volume);
                }
            }
        }

        public void SetLooping(bool loop)
        {
            u_Channel.loop = loop;
        }

        public void SetSpatialBlend(float blend)
        {
            u_Channel.spatialBlend = Mathf.Clamp01(blend);
        }

        public void SetPitch(float pitch)
        {
            u_Channel.pitch = pitch;
        }

        public void ToggleChannel(bool enabled)
        {
            u_Channel.mute = !enabled; 
        }
    }
}
