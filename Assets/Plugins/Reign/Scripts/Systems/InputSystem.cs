using System.Collections.Generic;
using Reign.Interfaces;
using Reign.Enums;
using UnityEngine;
using System;

namespace Reign.Systems
{
    public class InputSystem : System<InputSystem>, IDataHandler
    {
        public Dictionary<string, KeyCode[]> Inputs { get; private set; }

        public bool inputsEnabled { get; private set; } = false;

        public void SetInputsEnabled(bool isInputsEnabled)
        {
            inputsEnabled = isInputsEnabled;
        }

        public bool GetInput(string name, InputType inputType = InputType.DOWN)
        {
            if (!inputsEnabled) return false;

            if (name.Equals("any", StringComparison.CurrentCultureIgnoreCase))
            {
                return inputType switch
                {
                    InputType.DOWN => Input.anyKeyDown,
                    InputType.HELD => Input.anyKey,
                    _ => false
                };
            }

            if (Inputs.TryGetValue(name, out KeyCode[] keys))
            {
                foreach (KeyCode code in keys)
                {
                    bool wasPressed = inputType switch
                    {
                        InputType.DOWN => Input.GetKeyDown(code),
                        InputType.UP => Input.GetKeyUp(code),
                        InputType.HELD => Input.GetKey(code),
                        _ => false
                    };

                    return wasPressed;
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
            Inputs = DATA.inputMap.inputs;
        }

        public void SaveData(ref GameData DATA)
        {
            DATA.inputMap.inputs = Inputs;
        }
    }
}
