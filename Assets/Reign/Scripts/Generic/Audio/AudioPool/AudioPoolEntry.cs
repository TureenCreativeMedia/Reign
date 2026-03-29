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

        [AllowNesting] [Label("Is 3D")] public bool bool_3D = false;
        [AllowNesting] [Label("Should Loop")] public bool bool_Loop = false;

        [Space]

        [AllowNesting] [Label("Volume")] [Min(0)] public float float_Volume = 1.0f;
        [AllowNesting] [Label("Pitch")] public float float_Pitch = 1.0f;
        [AllowNesting] [Label("Clip")] public AudioClip AudioClip_RelativeClip;

        [Space]

        [Header("3D Settings")]
        [EnableIf("bool_3D")] [AllowNesting] [Label("Audio Rolloff Mode")] public AudioRolloffMode AudioRolloffMode_RolloffMode;
        [EnableIf("bool_3D")] [AllowNesting] [Label("Minimum Distance")] [Min(0)] public float float_MinimumDistance = 2.0f;
        [EnableIf("bool_3D")] [AllowNesting] [Label("Maximum Distance")] [Min(0)] public float float_MaximumDistance = 300.0f;
        [EnableIf("bool_3D")] [AllowNesting] [Label("Doppler Level")] [Min(0)] public float float_Doppler = 0.0f;
    }
}
