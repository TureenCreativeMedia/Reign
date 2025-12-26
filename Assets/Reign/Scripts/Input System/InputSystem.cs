using System;
using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    public class InputSystem : MonoBehaviour
    {
        /// <summary>
        /// A list containing all of the pressable input keys
        /// </summary>
        public static Dictionary<string, KeyCode[]> InputKeys = new()
        {
            // {"Template", new[]{KeyCode.None} },

            // Misc
            {"Any", null },

            // Grouped
            {"Alphanumeric", new[]{KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4,
            KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9} },

            // F Keys
            {"F2", new[]{KeyCode.F2} },
            {"F3", new[]{KeyCode.F3} },
            {"F5", new[]{KeyCode.F5} },
            {"F10", new[]{KeyCode.F10} },
            {"F11", new[]{KeyCode.F11} },
            {"F12", new[]{KeyCode.F12} },

            // Common
            {"Escape", new[]{KeyCode.Escape} },
            {"End", new[]{KeyCode.End} },
            {"Delete", new[]{KeyCode.Delete} },
            {"Home", new[]{KeyCode.Home} },
            {"Shift", new[]{KeyCode.LeftShift, KeyCode.RightShift} },
            {"Space", new[]{KeyCode.Space} },
            {"A", new[]{KeyCode.A} },
            {"S", new[]{KeyCode.S} },
            {"W", new[]{KeyCode.W} },
            {"D", new[]{KeyCode.D} },
            {"E", new[]{KeyCode.E} },
            {"F", new[]{KeyCode.F} },
            {"Q", new[]{KeyCode.Q} },
            {"C", new[]{KeyCode.C} },

            // Mouse
            {"LeftMouse", new[]{KeyCode.Mouse0} },
            {"RightMouse", new[]{KeyCode.Mouse1} },
            {"MiddleMouse", new[]{KeyCode.Mouse2} }
        };

        /// <summary>
        /// If inputs can be used
        /// </summary>
        public static bool inputsEnabled = true;

        /// <summary>
        /// The type of pressed key
        /// </summary>
        public enum KeyType { Down, Up, Held };

        /// <summary>
        /// Return a pressed key via name and KeyType
        /// </summary>
        public static bool GetInput(string inputName, KeyType type)
        {
            if (!InputKeys.TryGetValue(inputName, out KeyCode[] keys) || keys == null) return false;

            // Was any key pressed?
            if (inputName == "Any")
            {
                return type switch
                {
                    KeyType.Down => Input.anyKeyDown,
                    KeyType.Up => false,
                    KeyType.Held => Input.anyKey,
                    _ => false
                };
            }

            // Was a singular named key pressed?
            foreach (KeyCode key in InputKeys[inputName])
            {
                bool pressed = type switch
                {
                    KeyType.Down => Input.GetKeyDown(key),
                    KeyType.Up => Input.GetKeyUp(key),
                    KeyType.Held => Input.GetKey(key),
                    _ => false
                };
                if (pressed) return true;
            }

            return false;
        }

        /// <summary>
        /// Return the change of mouse direction as a Vector2
        /// </summary>
        public static Vector2 MouseLook()
        {
            if (!Input.mousePresent) return Vector2.zero;

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            if (mouseX != 0) mouseX *= 3;
            if (mouseY != 0) mouseY *= 3;

            return new(mouseX, mouseY);
        }

        /// <summary>
        /// Return if any key is held down
        /// </summary>
        public static bool AnyKeyHeld()
        {
            return GetInput("Any", KeyType.Held);
        }
    }
}
