using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reign
{
    [System.Serializable]
    public class AssetEntry<T>
    {
        [AllowNesting, Label("Key")] public string string_KEY;
        [AllowNesting, Label("Asset")] public T string_ASSET;
    }
}
