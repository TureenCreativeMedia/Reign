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
        [SerializeField] [Label("Audio Pool")] private AudioPool AudioPool_Pool;
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

        public static void Play(AudioSource SOURCE, string SOUNDNAME, Vector3? POSITION = null)
        {
            if (!Instance.Dictionary_AudioEntries.TryGetValue(SOUNDNAME, out AudioPoolEntry ENTRY)) return;
            
            SOURCE.clip = ENTRY.AudioClip_RelativeClip;
            SOURCE.loop = ENTRY.bool_Loop;
            SOURCE.volume = ENTRY.float_Volume;

            if (POSITION != null)
            {
                SOURCE.minDistance = ENTRY.float_MinimumDistance;
                SOURCE.maxDistance = ENTRY.float_MaximumDistance;
                SOURCE.rolloffMode = ENTRY.AudioRolloffMode_RolloffMode;
                SOURCE.gameObject.transform.position = POSITION.Value;
                SOURCE.spatialBlend = ENTRY.bool_3D ? 1.0f : 0.0f;
            }
            else
            {
                SOURCE.spatialBlend = 0.0f;
            }

            SOURCE.Play();
        }
    }
}