using NaughtyAttributes;
using UnityEngine;

namespace Reign.Backend
{
    public class EditorComment : MonoBehaviour
    {
        [TextArea(5, 15)] [Label("")] public string Comment;
    }
}
