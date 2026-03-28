using NaughtyAttributes;
using Reign.Generic;
using Reign.ScriptableObjects.Audio;
using System.Collections.Generic;
using UnityEngine;

namespace Reign.Systems.Audio
{
    public class AudioSystem : Singleton<AudioSystem>
    {
        [SerializeField] [Label("Audio Pool")] private AudioPool AudioPool_Pool;
        private Dictionary<string, AudioPoolEntry> Dictionary_AudioEntries = new();

        public override void Awake()
        {
            base.Awake();

            foreach (AudioPoolGroup GROUP in AudioPool_Pool.List_Groups)
            {
                foreach (AudioPoolEntry ENTRY in GROUP.List_AudioPoolEntries)
                {
                    Dictionary_AudioEntries[$"{GROUP.string_Name}.{ENTRY.string_Name}"] = ENTRY;
                }
            }
        }

        public static void Play(AudioSource SOURCE, string SOUNDNAME, Vector3 POSITION)
        {
            if (!Instance.Dictionary_AudioEntries.TryGetValue(SOUNDNAME, out AudioPoolEntry ENTRY)) return;

            SOURCE.gameObject.transform.position = POSITION;
            SOURCE.clip = ENTRY.AudioClip_RelativeClip;
            SOURCE.loop = ENTRY.bool_Loop;
            SOURCE.volume = ENTRY.float_Volume;
            SOURCE.minDistance = ENTRY.float_MinimumDistance;
            SOURCE.maxDistance = ENTRY.float_MaximumDistance;
            SOURCE.rolloffMode = ENTRY.AudioRolloffMode_RolloffMode;
            SOURCE.spatialBlend = ENTRY.bool_2D ? 0.0f : 1.0f;
            SOURCE.Play();
        }
    }
}