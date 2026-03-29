using NaughtyAttributes;
using UnityEngine;

namespace Reign.Backend
{
    public class EditorComment : MonoBehaviour
    {
#if UNITY_EDITOR
        [TextArea(5, 15)] [Label("")] public string Comment;
#endif
    }
}
