using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace reign
{
    public class ConsoleWidget : ReignWidget
    {
        [SerializeField] TMP_InputField tmp_InputField;
        [SerializeField] string s_ConsoleBinding = "BackQuote";
        [SerializeField] bool b_InConsole;

        private void OnEnable()
        {
            Main.a_OnFrame += ConsoleUpdate;
            tmp_InputField.onEndEdit.AddListener(DoConsoleCommand);
        }
        private void OnDisable()
        {
            Main.a_OnFrame -= ConsoleUpdate;
        }

        private void Awake()
        {
            ToggleConsole(false);
        }
        void ConsoleUpdate()
        {
            if (InputSystem.GetInput(s_ConsoleBinding, InputSystem.KeyType.Down))
            {
                ToggleConsole(!b_InConsole);
            }
        }
        void ToggleConsole(bool active)
        {
            b_InConsole = active;
            tmp_InputField.gameObject.SetActive(active);
            if (active) tmp_InputField.ActivateInputField();
        }

        void DoConsoleCommand(string s_CurrentCommand)
        {
            tmp_InputField.text = "";
            ToggleConsole(false);
            Debug.Log($"Console command: {s_CurrentCommand}");

            string[] parts = s_CurrentCommand.Split(" ");

            string command = parts[0];
            object value = null;

            if (parts.Length > 1)
            {
                value = parts[1];
            }

            // Evaluate Command
            switch (command.ToLower())
            {
                case "hang":
                    {
                        Main.Instance.Hang();
                        break;
                    }
            }
        }
    }
}
