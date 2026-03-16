using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    public static class Time
    {
        public static float float_UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;
        public static float float_DeltaTime => UnityEngine.Time.deltaTime;

        /// <summary>
        /// Convert time from seconds to MM:SS format
        /// </summary>
        public static string ConvertToMMSS(int SECONDS)
        {
            return $"{SECONDS / 60}:{SECONDS % 60:00}";
        }
    }
    public class AssetLibrary : MonoBehaviour
    {
        [System.Serializable]
        public class AssetEntry
        {
            public string string_KEY;
            public Object string_ASSET;
        }

        [SerializeField] private List<AssetEntry> AssetEntry_Assets = new();
        private Dictionary<string, Object> Dictionary_AssetDictionary;

        void Awake()
        {
            Dictionary_AssetDictionary = new Dictionary<string, Object>();

            foreach (AssetEntry ENTRY in AssetEntry_Assets)
            {
                if (!Dictionary_AssetDictionary.ContainsKey(ENTRY.string_KEY))
                {
                    Dictionary_AssetDictionary.Add(ENTRY.string_KEY, ENTRY.string_ASSET);
                }
                else
                {
                    Debug.LogWarning($"Duplicate asset key: {ENTRY.string_KEY}");
                }
            }
        }

        public T GetAsset<T>(string KEY) where T : Object
        {
            if (Dictionary_AssetDictionary.TryGetValue(KEY, out Object ASSET))
            {
                return ASSET as T;
            }

            Debug.LogWarning($"Asset {ASSET} not found: {KEY}");
            return null;
        }
    }

}
