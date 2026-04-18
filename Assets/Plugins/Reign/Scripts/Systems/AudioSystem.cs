using UnityEngine;
using Reign.Generics.Audio;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;

namespace Reign.Systems
{
    public class AudioSystem : System<AudioSystem>
    {
        [SerializeField] private AudioPool audioPool;
        [SerializeField] AudioMixerGroup defaultAudioMixerGroup;
        private readonly Dictionary<string, AudioPoolEntry> audioEntries = new();

        public void OnValidate()
        {
            foreach (AudioPoolGroup group in audioPool.audioGroups)
            {
                foreach (AudioPoolEntry entry in group.audioPoolEntries)
                {
                    audioEntries[$"{group.name}.{entry.name}"] = entry;
                }
            }
        }

        public bool HasSound(string SOUNDNAME)
        {
            return audioEntries != null && audioEntries.ContainsKey(SOUNDNAME);
        }

        public AudioPoolEntry GetEntry(string NAME)
        {
            AudioPoolEntry entry = null;
            audioEntries?.TryGetValue(NAME, out entry);

            return entry;
        }

        private AudioSource SourceSetup(AudioSource source, AudioPoolEntry entry, Vector3? pos, bool loop = false)
        {
            source.loop = loop;
            source.volume = entry.localVolume;

            if (pos != null)
            {
                source.transform.position = pos.Value;
                source.spatialBlend = entry.spatialBlend;
            }
            else
            {
                source.spatialBlend = 0.0f;
            }

            return source;
        }

        public void Play(AudioSource source, string name, Vector3? pos, int index = 0, bool loop = false)
        {
            var entry = GetEntry(name);
            if (entry == null) return;

            SourceSetup(source, entry, pos);
            source.clip = entry.clips[index];
            source.Play();
        }

        public void PlayOneShot(AudioSource source, string name, Vector3? pos, int index = 0)
        {
            var entry = GetEntry(name);
            if (entry == null) return;

            SourceSetup(source, entry, pos);
            source.PlayOneShot(entry.clips[index]);
        }

        public AudioSource PlayCreateInstance(string name, Vector3? pos, bool destroyOnComplete = true)
        {
            GameObject newSound = new(name);
            AudioSource source = newSound.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = defaultAudioMixerGroup;
            Play(source, name, pos);

            if (destroyOnComplete)
            {
                StartCoroutine(DestroyOnComplete(source));
            }

            return source;
        }

        private IEnumerator DestroyOnComplete(AudioSource SOURCE)
        {
            yield return new WaitUntil(() => !SOURCE.isPlaying);
            Destroy(SOURCE.gameObject);
        }
    }
}
