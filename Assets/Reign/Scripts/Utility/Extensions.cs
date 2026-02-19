using UnityEngine;

namespace reign
{
    public interface IDestroyableTransform
    {
        void DestroyTransform(Transform TRANSFORM);
    }
    public interface IDamagable
    {
        void Damage(float STRENGTH);
        void Destroy();
    }
    public static class Time
    {
        public static float float_UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;
        public static float float_DeltaTime => UnityEngine.Time.deltaTime;

        /// <summary>
        /// Convert time from seconds to MM:SS format
        /// </summary>
        public static string ConvertToMMSS(int SECONDS)
        {
            return $"{SECONDS / 60}:{SECONDS % 60}";
        }
    }
}
