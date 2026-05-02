using System.Collections.Generic;
using Reign.Interfaces;
using Reign.Enums;
using UnityEngine;
using System;
using Reign.Generic.Saving;
using Reign.Generic.Input;

namespace Reign.Systems
{
    public sealed class InputSystem : System<InputSystem>, IDataHandler
    {
        public Dictionary<string, KeyCode[]> Inputs { get; private set; } = new();

        public bool InputsEnabled { get; private set; } = false;

        /// <summary>
        /// Toggle the ability to use GetInput()
        /// </summary>
        public void SetInputsEnabled(bool isInputsEnabled)
        {
            InputsEnabled = isInputsEnabled;
        }

        /// <summary>
        /// Check if the input of this name was down, up or held this frame (depending on the InputType)
        /// </summary>
        public bool GetInput(string name, InputType inputType = InputType.DOWN, bool createIfMissing = false, KeyCode[] inputDefaults = null)
        {
            if (!InputsEnabled || Inputs == null) return false;

            if (name.Equals("any", StringComparison.CurrentCultureIgnoreCase))
            {
                return inputType switch
                {
                    InputType.DOWN => Input.anyKeyDown,
                    InputType.HELD => Input.anyKey,
                    _ => false
                };
            }

            if (!Inputs.TryGetValue(name, out KeyCode[] keys))
            {
                if (!createIfMissing) return false;

                if (inputDefaults == null || inputDefaults.Length == 0)
                {
                    throw new Exception("Cannot create missing inputs if there are no inputDefaults to set it to.");
                }

                keys = (KeyCode[])inputDefaults.Clone();
                Inputs[name] = keys;
            }

            if (inputType == InputType.COMPOSITE)
            {
                if (keys == null || keys.Length == 0) return false;

                foreach (KeyCode code in keys)
                {
                    if (!Input.GetKey(code)) return false;
                }

                return true;
            }

            foreach (KeyCode code in keys)
            {
                if (inputType switch
                {
                    InputType.DOWN => Input.GetKeyDown(code),
                    InputType.UP => Input.GetKeyUp(code),
                    InputType.HELD => Input.GetKey(code),
                    _ => false
                })
                {
                    return true;
                }
            }

            return false;
        }

        private void Start()
        {
            SetInputsEnabled(true);
        }

        public void LoadData(GameData DATA)
        {
            DATA.inputMap ??= new InputMap();
            if (DATA.inputMap.Inputs == null) return;

            Inputs = new Dictionary<string, KeyCode[]>();

            foreach (var pair in DATA.inputMap.Inputs)
            {
                Inputs[pair.Key] = pair.Value != null ? (KeyCode[])pair.Value.Clone() : Array.Empty<KeyCode>();
            }
        }

        public void SaveData(ref GameData DATA)
        {
            DATA.inputMap.OverwriteAllInputs(Inputs);
        }
    }
}