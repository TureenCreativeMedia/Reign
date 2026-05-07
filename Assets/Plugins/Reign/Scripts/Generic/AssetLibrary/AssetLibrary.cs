using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Reign.Generic.AssetLibrary
{
    public abstract class AssetLibrary<T> : MonoBehaviour where T : Object
    {
        private List<AssetEntry<T>> assetEntries = new();
        private Dictionary<string, T> assets;

        private void Awake() => Validate();

        private void Validate()
        {
            assets = new();

            foreach (var asset in assetEntries)
            {
                if (!assets.TryAdd(asset.key, asset.asset))
                {
                    Debug.LogWarning($"Duplicate asset key detected at {asset.key}");
                }
            }
        }

        /// <summary>
        /// Get values of the dictionary as a List<T>
        /// </summary>
        public List<T> GetValues()
        {
            if (assets == null)
            {
                Validate();
            }

            return assets.Values.ToList();
        }

        /// <summary>
        /// Return asset T by name in dictionary.
        /// </summary>
        public T GetAssetByName(string name)
        {
            if (assets == null)
            {
                Validate();
            }

            if (assets.TryGetValue(name, out T asset))
            {
                return asset;
            }

            Debug.LogWarning($"Asset {typeof(T)} of name '{name}' not found in AssetLibrary dictionary");
            return default;
        }
    }
}