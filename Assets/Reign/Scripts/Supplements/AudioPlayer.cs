using NaughtyAttributes;
using Reign.Systems.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reign
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        private AudioSource AudioSource_Reference;
        [SerializeField, Label("Autoplay Sound"), ValidateInput("SoundExists", "Sound System does not contain a definition for this clip name")] string string_Autoplay;

        private bool SoundExists(string SOUND)
        {
            return string.IsNullOrEmpty(SOUND) || AudioSystem.HasSound(SOUND);
        }

        private void Awake()
        {
            AudioSource_Reference = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (string.IsNullOrEmpty(string_Autoplay)) return;
            PlaySound(string_Autoplay);
        }

        public void PlaySound(string CLIP, Vector3? POSITION = null)
        {
            AudioSystem.Play(AudioSource_Reference, CLIP, POSITION);
        }
        public void PlaySoundOneShot(string CLIP, Vector3? POSITION = null)
        {
            AudioSystem.PlayOneShot(AudioSource_Reference, CLIP, POSITION);
        }
    }
}
