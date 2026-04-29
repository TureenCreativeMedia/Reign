using System;
using NaughtyAttributes;
using UnityEngine;

namespace Reign.Generic.Audio
{
    [Serializable]
    public class AudioPoolEntry
    {
        [Header("Basic Settings")]
        [AllowNesting] public string name;
        [AllowNesting, Range(0, 1)] public float localVolume = 0.8f;
        [AllowNesting, Range(-3, 3)] public float localPitch = 1.0f;
        [AllowNesting] public AudioClip[] clips;

        [Space(5)]
        [Header("Advanced (3D Settings)")]

        [SerializeField] bool advanced;
        [AllowNesting, EnableIf("advanced")] public float spatialBlend = 0.0f;
        [AllowNesting, EnableIf("advanced")] public AudioRolloffMode audioRolloffMode = AudioRolloffMode.Linear;
        [AllowNesting, EnableIf("advanced")] public float minDistance = 0;
        [AllowNesting, EnableIf("advanced")] public float maxDistance = 30;
        [AllowNesting, EnableIf("advanced"), Range(0, 5)] public float dopplerLevel = 1.0f;
    }
}