using System.Collections.Generic;
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
                b_ChannelActive = true;
            }
            else
            {
                b_ChannelActive = true;
            }
        }
        public void ToggleAudioChannel()
        { 
            b_ChannelActive = !b_ChannelActive;
        }
        public void StopChannel()
        {
            u_Channel.Stop();
        }

        public void PauseChannel()
        {
            u_Channel.Pause();
        }
    }
}
