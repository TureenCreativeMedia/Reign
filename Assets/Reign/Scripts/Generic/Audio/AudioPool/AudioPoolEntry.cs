using System;
using UnityEngine;

namespace Reign.ScriptableObjects.Audio
{
    [Serializable]
    public class AudioPoolEntry
    {
        [Header("Audio Settings")]
        public string string_Name;

        [Space]

        public bool bool_2D = false;
        public bool bool_Loop = false;

        [Space]

        public float float_Volume = 1.0f;
        public float float_Pitch = 1.0f;
        public AudioRolloffMode AudioRolloffMode_RolloffMode;
        public AudioClip AudioClip_RelativeClip;

        [Space]

        [Header("3D Settings")]
        public float float_MinimumDistance = 2.0f;
        public float float_MaximumDistance = 300.0f;
        public float float_Doppler = 0.0f;
    }
}
