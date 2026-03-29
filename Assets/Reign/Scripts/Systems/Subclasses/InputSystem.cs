using Reign.Generic;
using Reign.Systems.Save;
using System.Collections.Generic;
using UnityEngine;

namespace Reign.Systems.Controls
{
    public enum InputType 
    { 
        Down, 
        Held, 
        Up 
    }
    public class InputSystem : Singleton<InputSystem>, IDataHandler
    {
        private Dictionary<string, KeyCode[]> Dictionary_BindedInputsClone;
        private bool bool_DisableInputs = false;
        
        void Start()
        {
            DisableInputs(false);
        }

        public bool GetKey(string KEY, InputType TYPE)
        {
            if (bool_DisableInputs) return false;

            if (KEY == "Any")
            {
                return TYPE switch
                {
                    InputType.Down => Input.anyKeyDown,
                    InputType.Held => Input.anyKey,
                    _ => false
                };
            }

            if (Dictionary_BindedInputsClone.TryGetValue(KEY, out KeyCode[] KEYS))
            {
                foreach (KeyCode KEYCODE in KEYS)
                {
                    bool bool_WasPressed = TYPE switch
                    {
                        InputType.Down => Input.GetKeyDown(KEYCODE),
                        InputType.Up => Input.GetKeyUp(KEYCODE),
                        InputType.Held => Input.GetKey(KEYCODE),
                        _ => false
                    };

                    if (bool_WasPressed) return true;
                }
            }

            return false;
        }
        public static void DisableInputs(bool STATE)
        {
            Instance.bool_DisableInputs = STATE;
        }
        public void TryRebind(string KEY, KeyCode[] KEYCODES, bool REPLACEEXISTING)
        {
            if (REPLACEEXISTING && Dictionary_BindedInputsClone.ContainsKey(KEY))
            {
                Dictionary_BindedInputsClone[KEY] = KEYCODES;
            }
            else
            {
                Dictionary_BindedInputsClone.Add(KEY, KEYCODES);
            }

            SaveSystem.Instance.SaveGameData();
        }
        public void LoadData(GameData DATA)
        {
            Dictionary_BindedInputsClone = DATA.Dictionary_BindedInputs;
        }

        public void SaveData(ref GameData DATA)
        {
            DATA.Dictionary_BindedInputs = Dictionary_BindedInputsClone;
        }
    }
}
