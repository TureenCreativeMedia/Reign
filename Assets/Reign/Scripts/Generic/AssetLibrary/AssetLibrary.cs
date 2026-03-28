using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Reign
{
    public class AssetLibrary<T> : MonoBehaviour where T : Object
    {
        private protected bool bool_Initialised;
        [SerializeField] [Label("Assets")] private List<AssetEntry<T>> AssetEntry_Assets = new();
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

            foreach (AssetEntry<T> ENTRY in AssetEntry_Assets)
            {
                if (!Dictionary_AssetDictionary.ContainsKey(ENTRY.string_KEY))
                {
                    Dictionary_AssetDictionary[ENTRY.string_KEY] = ENTRY.string_ASSET;
                }
                else
                {
                    Debug.LogWarning($"Duplicate asset key: {ENTRY.string_KEY}");
                }
            }
        }
        public bool ContainsKey(string KEY)
        {
            Initialise();
            return Dictionary_AssetDictionary.ContainsKey(KEY);
        }

        public List<T> GetValues()
        {
            return Dictionary_AssetDictionary.Values as List<T>;
        }

        public T GetAsset(string KEY)
        {
            Initialise();

            if (Dictionary_AssetDictionary.TryGetValue(KEY, out T ASSET))
            {
                return ASSET;
            }

            Debug.LogWarning($"Asset {ASSET.GetType()} not found: {KEY}");
            return null;
        }
    }
}