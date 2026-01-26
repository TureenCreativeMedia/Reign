using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    public class Audio : MonoBehaviour
    {
        // Used for the Reign audio system.

        public static Audio Instance { get; private set; }
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

            SetAudioChannelsActive(true);
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
            foreach (var channel in r_Channels)
            {
                // Stops every channel
                channel.StopChannel();
            }
        }

        public void ToggleAllChannels()
        {
            foreach (var channel in r_Channels)
            {
                channel.ToggleAudioChannel();
            }
        }
        public void SetAudioChannelsActive(bool enabled)
        {
            foreach (var channel in r_Channels)
            {
                channel.b_ChannelActive = enabled;
            }
        }

        public void Play(int channelID, string soundName, float volume = 0.7f, bool loop = false)
        {
            if (channelID <= 0 || channelID > r_Channels.Length) return;
            if (!r_Channels[channelID - 1].b_ChannelActive) return;

            for (int i = 0; i < r_Channels[channelID - 1].r_Pool.u_AudioClips.Count; ++i)
            {
                if (r_Channels[channelID - 1].r_Pool.u_AudioClips[i].name == soundName)
                {
                    var channel = r_Channels[channelID - 1];

                    channel.u_Channel.loop = true;
                    channel.u_Channel.clip = channel.r_Pool.u_AudioClips[i];
                    channel.u_Channel.volume = volume;
                    channel.u_Channel.Play();

                    return;
                }
            }
        }
        public void PlayOneShot(string soundName, float volume = 0.7f)
        {
            string[] s_grouping = soundName.Split(".");

            if (!soundName.Contains('.')) return;

            // Refer to sounds by playing with ChannelName.SoundName

            for (int i = 0; i < r_Channels.Length; ++i)
            {
                if (r_Channels[i].s_ChannelName == s_grouping[0])
                {
                    if (!r_Channels[i].b_ChannelActive)
                        continue;

                    for (int j = 0; j < r_Channels[i].r_Pool.u_AudioClips.Count; ++j)
                    {
                        if (r_Channels[i].r_Pool.u_AudioClips[j].name == s_grouping[1])
                        {
                            r_Channels[i].u_Channel.PlayOneShot(r_Channels[i].r_Pool.u_AudioClips[j], volume);
                        }
                    }
                }
            }
        }
    }
}
