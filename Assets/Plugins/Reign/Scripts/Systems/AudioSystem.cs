using UnityEngine;
using Reign.Generic.Saving;
using Reign.Generic.Audio;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;
using Reign.Interfaces;
using System.Threading.Tasks;

namespace Reign.Systems
{
    public sealed class AudioSystem : System<AudioSystem>, IDataHandler
    {
        [SerializeField] private AudioPool audioPool;
        [SerializeField] AudioMixerGroup masterAudioMixerGroup;
        private readonly Dictionary<string, AudioPoolEntry> audioEntries = new();

        public void OnValidate()
        {
            if (audioPool == null || audioEntries == null) return;

            foreach (AudioPoolGroup group in audioPool.audioGroups)
            {
                foreach (AudioPoolEntry entry in group.audioPoolEntries)
                {
                    audioEntries[$"{group.name}.{entry.name}"] = entry;
                }
            }
        }

        /// <summary>
        /// Check if audio entries dictionary contains the key of name
        /// </summary>
        /// <param name="name">Audio entry key</param>
        public bool HasSound(string name)
        {
            return audioEntries != null && audioEntries.ContainsKey(name);
        }

        /// <summary>
        /// Return entry in audio entries dictionary by name
        /// </summary>
        /// <param name="name"></param>
        public AudioPoolEntry GetEntry(string name)
        {
            AudioPoolEntry entry = null;                // Initialise as null
            audioEntries?.TryGetValue(name, out entry); // Try get value and output entry

            return entry;
        }

        /// <summary>
        /// Set up the source depending on parameters
        /// </summary>
        /// <param name="source">Required audio source</param>
        /// <param name="entry">Audio pool entry to parse data from</param>
        /// <param name="pos">Nullable Vector3 to place the source transform position at</param>
        /// <param name="loop">If the source should loop</param>
        private static void SourceSetup(AudioSource source, AudioPoolEntry entry, Vector3? pos, bool loop = false)
        {
            source.loop = loop;
            source.volume = entry.localVolume;

            if (pos.HasValue)
            {
                source.transform.position = pos.Value;
                source.spatialBlend = entry.spatialBlend;
            }
            else
            {
                source.spatialBlend = 0.0f;
            }

            return;
        }

        /// <summary>
        /// Play an audio pool entry with the same name from a given source
        /// </summary>
        /// <param name="source">Source to play from</param>
        /// <param name="name">Matching audio pool entry name</param>
        /// <param name="pos">Nullable Vector3 to play at</param>
        /// <param name="index">Index of entry's clip array to pull from</param>
        /// <param name="loop">If the source should loop</param>
        public void Play(AudioSource source, string name, Vector3? pos, int index = 0, bool loop = false)
        {
            var entry = GetEntry(name);
            if (entry == null) return;

            SourceSetup(source, entry, pos);
            source.clip = entry.clips[index];
            source.Play();
        }

        /// <summary>
        /// Play one shot audio pool entry with the same name from a source. For looping, use Play()
        /// </summary>
        /// <param name="source">Source to play from</param>
        /// <param name="name">Matching audio pool entry name</param>
        /// <param name="pos">Nullable Vector3 to play at</param>
        /// <param name="index">Index of entry's clip array to pull from</param>
        public void PlayOneShot(AudioSource source, string name, Vector3? pos, int index = 0)
        {
            var entry = GetEntry(name);
            if (entry == null) return;

            SourceSetup(source, entry, pos);
            source.PlayOneShot(entry.clips[index]);
        }

        /// <summary>
        /// Create a new GameObject with an audio source and play the audio pool entry with the same name
        /// </summary>
        /// <param name="name">Matching audio pool entry name</param>
        /// <param name="pos">Nullable Vector3 to play at</param>
        /// <param name="index">Index of entry's clip array to pull from</param>
        /// <param name="loop">If the source should loop</param>
        /// <param name="destroyOnComplete">If the game object should be destroyed when the sound is finished</param>
        public AudioSource PlayCreateInstance(string name, Vector3? pos, int index = 0, bool loop = false, bool destroyOnComplete = true)
        {
            GameObject newSound = new($"Sound Instance ({name})");
            AudioSource source = newSound.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = masterAudioMixerGroup;
            Play(source, name, pos, index, loop);

            if (destroyOnComplete)
            {
                DestroyOnComplete(source);
            }

            return source;
        }

        private async void DestroyOnComplete(AudioSource source)
        {
            while (source != null && source.isPlaying)
            {
                await Task.Yield();
            }

            if (source != null)
            {
                Object.Destroy(source.gameObject);
            }
        }

        public void LoadData(GameData DATA)
        {
            // masterAudioVolume should save and load in dB
            masterAudioMixerGroup.audioMixer.SetFloat("Volume", DATA.masterAudioVolume);
        }

        public void SaveData(ref GameData DATA)
        {
        }
    }
}
