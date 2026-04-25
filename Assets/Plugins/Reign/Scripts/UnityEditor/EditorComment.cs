#pragma warning disable CS0414 // The private field is assigned but its value is never used
#if UNITY_EDITOR

using NaughtyAttributes;
using UnityEngine;

namespace Reign.Editor
{
    public sealed class EditorComment : MonoBehaviour
    {
        [TextArea(10, 20), SerializeField, Label("")] string comment;

        void Awake()
        {
            // Clear on Awake()
            comment = null;
        }
    }
}

#endif