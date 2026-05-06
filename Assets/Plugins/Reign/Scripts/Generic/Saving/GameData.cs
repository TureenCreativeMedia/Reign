using System.Collections.Generic;
using Reign.Generic.Visuals;
using UnityEngine;

namespace Reign.Generic.Saving
{
    [System.Serializable]
    public sealed class GameData
    {
        public ScreenSettings screenResolution;

        public float masterAudioVolume;

        public GameData()
        {
            screenResolution = new ScreenSettings(1280, 720, false, true, true);

            masterAudioVolume = -2.0f; // in Decibels
        }
    }
}