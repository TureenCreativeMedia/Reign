using UnityEngine;

namespace Reign.Backend
{
    public class EditorComment : MonoBehaviour
    {
        [TextArea(4, 8)]
        public string Comment;
    }
}
