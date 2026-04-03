using NaughtyAttributes;
using Reign.Generic;
using Reign.ScriptableObjects.Audio;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Reign.Systems.Audio
{
    public class AudioSystem : Singleton<AudioSystem>
    {
        [SerializeField][Label("Audio Pool")] private AudioPool AudioPool_Pool;
        private readonly Dictionary<string, AudioPoolEntry> Dictionary_AudioEntries = new();

        public void OnValidate()
        {
            foreach (AudioPoolGroup GROUP in AudioPool_Pool.List_Groups)
            {
                foreach (AudioPoolEntry ENTRY in GROUP.List_AudioPoolEntries)
                {
                    Dictionary_AudioEntries[$"{GROUP.string_Name}.{ENTRY.string_Name}"] = ENTRY;
                }
            }
        }

        public static bool HasSound(string SOUNDNAME)
        {
            return Instance.Dictionary_AudioEntries.ContainsKey(SOUNDNAME);
        }

        private static void SetupSource(AudioSource SOURCE, AudioPoolEntry ENTRY, Vector3? POSITION)
        {
            SOURCE.loop = ENTRY.bool_Loop;
            SOURCE.volume = ENTRY.float_Volume;

            if (POSITION != null)
            {
                SOURCE.transform.position = POSITION.Value;
                SOURCE.spatialBlend = ENTRY.bool_3D ? 1f : 0f;
            }
            else
            {
                SOURCE.spatialBlend = 0f;
            }
        }

        public static AudioSource Play(AudioSource SOURCE, string SOUNDNAME, Vector3? POSITION = null)
        {
            if (!Instance.Dictionary_AudioEntries.TryGetValue(SOUNDNAME, out AudioPoolEntry ENTRY)) return SOURCE;

            SetupSource(SOURCE, ENTRY, POSITION);
            SOURCE.clip = ENTRY.AudioClip_RelativeClip;
            SOURCE.Play();

            return SOURCE;
        }

        public static AudioSource PlayOneShot(AudioSource SOURCE, string SOUNDNAME, Vector3? POSITION = null)
        {
            if (!Instance.Dictionary_AudioEntries.TryGetValue(SOUNDNAME, out AudioPoolEntry ENTRY)) return SOURCE;

            SetupSource(SOURCE, ENTRY, POSITION);
            SOURCE.PlayOneShot(ENTRY.AudioClip_RelativeClip);

            return SOURCE;
        }
        public static AudioSource PlayCreateInstance(string SOUNDNAME, Vector3? POSITION)
        {
            GameObject GameObject_NewSound = new(SOUNDNAME);
            AudioSource AudioSource_Source = GameObject_NewSound.AddComponent<AudioSource>();
            return Play(AudioSource_Source, SOUNDNAME, POSITION);
        }
    }
}