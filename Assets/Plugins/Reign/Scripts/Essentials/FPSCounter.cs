using System.Collections.Generic;
using UnityEngine;

namespace Reign.Essentials
{
    public class FPSCounter : MonoBehaviour
    {
        // Update Rate (seconds)
        [SerializeField] private float updateInterval = 1f;

        // If average FPS should be logged
        [SerializeField] bool shouldLog;

        // If average should be tracked
        [SerializeField] bool trackAverage;

        private readonly List<float> fpsSamples = new();
        private float fps;

        private float elapsed = 0.0f;
        private int frameCount = 0;

        private void Update()
        {
            elapsed += Time.unscaledDeltaTime;
            frameCount++;

            if (elapsed >= updateInterval)
            {
                fps = frameCount / elapsed;

                if (trackAverage)
                {
                    if (fpsSamples.Count > 90)
                    {
                        fpsSamples.RemoveAt(0);
                    }

                    fpsSamples.Add(fps);
                }

                if (shouldLog)
                {
                    Debug.Log(trackAverage ? $"Current FPS: {fps} | Average FPS: {GetAverageFPS(fpsSamples)}" : $"Current FPS: {fps}");
                }
                frameCount = 0;
                elapsed = 0.0f;
            }
        }

        private float GetAverageFPS(List<float> samples)
        {
            if (samples.Count == 0 || !trackAverage) return 0f;

            float total = 0f;
            foreach (float s in samples)
            {
                total += s;
            }

            return total / samples.Count;
        }
    }
}