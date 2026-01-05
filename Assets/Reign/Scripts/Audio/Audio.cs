using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    public class Audio : MonoBehaviour
    {
        // Used for the Reign audio system.

        public static Audio Instance;
        public MasterChannel[] r_Channels;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                // Should be a child of App, so doesn't need to persist as App print already does.
            }
        }

        public MasterChannel[] GetAudioChannels()
        {
            List<MasterChannel> r_Temp = new();
            for (int i = 0; i < r_Channels.Length; ++i)
            {
                r_Temp.Add(r_Channels[i]);
            }
            return r_Temp.ToArray();
        }

        public void StopAllSounds()
        {
            foreach (MasterChannel channel in GetAudioChannels())
            {
                // Stops every channel
                channel.u_Channel.Stop();
            }
        }

        public void ToggleAudioChannels()
        {
            foreach (MasterChannel channel in GetAudioChannels())
            {
                channel.b_ChannelActive = !channel.b_ChannelActive;
            }
        }

        public void SetAudioChannelsActive(bool enabled)
        {
            foreach (MasterChannel channel in GetAudioChannels())
            {
                channel.b_ChannelActive = enabled;
            }
        }

        public void PlaySound(string soundName, float volume = 0.7f)
        {
            string[] s_grouping = soundName.Split(".");

            // Refer to sounds by playing with ChannelName.SoundName

            for (int i = 0; i < r_Channels.Length; ++i)
            {
                if (r_Channels[i].s_ChannelName == s_grouping[0])
                {
                    if (!r_Channels[i].b_ChannelActive)
                    {
                        return;
                    }

                    for (int j = 0; j < r_Channels[i].r_Pool.u_AudioClips.Count; ++j)
                    {
                        if (r_Channels[i].r_Pool.u_AudioClips[j].name == soundName)
                        {
                            r_Channels[i].u_Channel.PlayOneShot(r_Channels[i].r_Pool.u_AudioClips[j], volume);
                        }
                    }
                }
            }
        }
    }
}
