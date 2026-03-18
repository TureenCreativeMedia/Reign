using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    public class AssetLibrary<T> : MonoBehaviour where T : Object
    {
        [System.Serializable]
        public class AssetEntry
        {
            public string string_KEY;
            public T string_ASSET;
        }

        private protected bool bool_Initialised;
        [SerializeField] private List<AssetEntry> AssetEntry_Assets = new();
        private Dictionary<string, T> Dictionary_AssetDictionary;

        private void Awake()
        {
            bool_Initialised = false;
            Initialise();
        }
        void Initialise()
        {
            if (bool_Initialised) return;

            Dictionary_AssetDictionary = new Dictionary<string, T>();

            foreach (AssetEntry ENTRY in AssetEntry_Assets)
            {
                if (!Dictionary_AssetDictionary.ContainsKey(ENTRY.string_KEY))
                {
                    Dictionary_AssetDictionary.Add(ENTRY.string_KEY, ENTRY.string_ASSET);
                }
                else
                {
                    Logger.Instance.Log(Logger.enum_LogIntensity.Warning, $"Duplicate asset key: {ENTRY.string_KEY}");
                }
            }
        }
        public bool ContainsKey(string KEY)
        {
            Initialise();
            return Dictionary_AssetDictionary.ContainsKey(KEY);
        }

        public T GetAsset(string KEY)
        {
            Initialise();

            if (Dictionary_AssetDictionary.TryGetValue(KEY, out T ASSET))
            {
                return ASSET;
            }

            Logger.Instance.Log(Logger.enum_LogIntensity.Warning, $"Asset {ASSET.GetType()} not found: {KEY}");
            return null;
        }
    }
}