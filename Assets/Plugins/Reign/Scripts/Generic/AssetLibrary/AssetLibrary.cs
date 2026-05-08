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
        /// Get first value in asset entries of type T
        /// </summary>
        public Type GetFirstValueOfType<Type>()
        {
            return GetValuesOfType<Type>().FirstOrDefault();
        }

        /// <summary>
        /// Get list of values of type T
        /// </summary>
        public List<Type> GetValuesOfType<Type>()
        {
            if (assets == null)
            {
                Validate();
            }

            return assets.Values.OfType<Type>().ToList();
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