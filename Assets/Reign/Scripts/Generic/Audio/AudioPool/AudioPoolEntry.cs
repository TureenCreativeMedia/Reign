using NaughtyAttributes;
using System;
using UnityEngine;

namespace Reign.ScriptableObjects.Audio
{
    [Serializable]
    public class AudioPoolEntry
    {
        [Header("Audio Settings")]
        [AllowNesting] [Label("Name")] public string string_Name;

        [Space]

        [AllowNesting] [Label("Is 2D")] public bool bool_2D = false;
        [AllowNesting] [Label("Should Loop")] public bool bool_Loop = false;

        [Space]

        [AllowNesting] [Label("Volume")] public float float_Volume = 1.0f;
        [AllowNesting] [Label("Pitch")] public float float_Pitch = 1.0f;
        [AllowNesting] [Label("Audio Rolloff Mode")] public AudioRolloffMode AudioRolloffMode_RolloffMode;
        [AllowNesting] [Label("Clip")] public AudioClip AudioClip_RelativeClip;

        [Space]

        [Header("3D Settings")]
        [AllowNesting] [Label("Minimum Distance")] public float float_MinimumDistance = 2.0f;
        [AllowNesting] [Label("Maximum Distance")] public float float_MaximumDistance = 300.0f;
        [AllowNesting] [Label("Doppler Level")] public float float_Doppler = 0.0f;
    }
}
