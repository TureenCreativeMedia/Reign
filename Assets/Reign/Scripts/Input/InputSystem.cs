using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    public class InputSystem : MonoBehaviour, IDataHandler
    {
        public static Dictionary<string, KeyCode[]> Dictionary_InputKeys;

        public static bool bool_InputsEnabled = false;

        public enum enum_KeyType { Down, Up, Held };

        /// <summary>
        /// 
        /// </summary>
        public static bool GetInput(string KEY, enum_KeyType TYPE, bool IGNOREDISABLED = false)
        {
            if (!bool_InputsEnabled && !IGNOREDISABLED) return false;

            if (KEY == "Any")
            {
                return TYPE switch
                {
                    enum_KeyType.Down => Input.anyKeyDown,
                    enum_KeyType.Up => false,
                    enum_KeyType.Held => Input.anyKey,
                    _ => false
                };
            }

            Dictionary_InputKeys.TryGetValue(KEY, out KeyCode[] keys);

            foreach (KeyCode key in keys)
            {
                bool pressed = TYPE switch
                {
                    enum_KeyType.Down => Input.GetKeyDown(key),
                    enum_KeyType.Up => Input.GetKeyUp(key),
                    enum_KeyType.Held => Input.GetKey(key),
                    _ => false
                };

                if (pressed)
                    return true;
            }

            return false;
        }

        public static Vector2 GetMouseVector()
        {
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        public static void Rebind(string KEY, KeyCode[] INPUTS, bool ONLYEXISTING = true)
        {
            if (!ONLYEXISTING || Dictionary_InputKeys.ContainsKey(KEY))
            {
                Dictionary_InputKeys[KEY] = INPUTS;
            }
            SaveSystem.Action_AttemptSave?.Invoke();
        }
        public void LoadData(GameData DATA)
        {
            Dictionary_InputKeys = new Dictionary<string, KeyCode[]>();

            foreach (KeyBinding BINDING in DATA.List_InputKeys)
            {
                Dictionary_InputKeys[BINDING.Key] = BINDING.KeyCodes;
            }

            bool_InputsEnabled = true;
        }

        public void SaveData(ref GameData DATA)
        {
            DATA.List_InputKeys = new List<KeyBinding>();

            foreach (var PAIR in Dictionary_InputKeys)
            {
                KeyCode[] copy = PAIR.Value.Clone() as KeyCode[];
                DATA.List_InputKeys.Add(new KeyBinding(PAIR.Key, copy));
            }
        }
    }
}
