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
        private void Awake()
        {
            AudioSource_Reference = GetComponent<AudioSource>();
        }
        public void PlaySound(string CLIP)
        {
            AudioSystem.Play(AudioSource_Reference, CLIP, Vector3.positiveInfinity);
        }
        public void PlaySound3D(string CLIP, Vector3 POSITION)
        {
            AudioSystem.Play(AudioSource_Reference, CLIP, POSITION);
        }
    }
}
