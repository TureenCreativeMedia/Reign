using System;
using System.IO;
using UnityEngine;

namespace Reign.ScriptableObjects.Cursor
{
    [Serializable, CreateAssetMenu(fileName = "New Cursor Setting", menuName = "Reign/New Cursor Setting")]
    public class CursorSetting : ScriptableObject
    {
        public string string_ID;
        public Texture2D Texture2D_Texture;
        public Vector2 Vector2_Hotspot;
    }
}