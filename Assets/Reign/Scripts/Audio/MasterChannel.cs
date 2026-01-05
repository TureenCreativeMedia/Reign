using UnityEngine;

namespace reign
{
    [RequireComponent(typeof(AudioSource))]
    public class MasterChannel : MonoBehaviour
    {
        public string s_ChannelName; // Name of channel for playing sounds
        public bool b_ChannelActive = true; // Channel enabled
        public AudioSource u_Channel; // Is the channel source
        public AudioPool r_Pool; // Contains sounds

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
