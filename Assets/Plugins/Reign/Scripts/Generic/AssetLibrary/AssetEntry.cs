namespace Reign.Generic.AssetLibrary
{
    [System.Serializable]
    public class AssetEntry<T>
    {
        public string key;
        public T asset;
    }
}