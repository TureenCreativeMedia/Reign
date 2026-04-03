using Reign.AssetLibrary;
using UnityEngine;

namespace Reign.Main
{
    public class ReignPropertyCallbacks
    {
        public Reign Reign => Reign.Instance;

        public int GetAssetLibraryCount<T>() where T : Object
        {
            return GameObject.FindObjectsOfType<AssetLibrary<T>>().Length;
        }
    }
}
