using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace reign
{
    public class ConsoleWidget : ReignWidget
    {
        [SerializeField] TMP_InputField tmp_InputField;
        [SerializeField] TMP_Text tmp_CommandList;
        [SerializeField] string s_ConsoleBinding = "BackQuote";
        [SerializeField] bool b_InConsole;
        string s_LastCommand;

        readonly List<string> l_CommandList = new()
        {
            "Open [{Scene name}, Self] - Load scene",
            "Hang - Close app",
            "PlaytimeLimiter [Warn, {Timer value}] - Interact with the Playtime Limiter",
            "Profiler - Toggle profiler",
            "Discord [Disconnect] - Interact with the Discord Controller",
            "SoundSystem [Stop, Toggle, Play {Clip name}] - Interact with the sound system",
            "Screen [Fullscreen, SetWidth {screen width}, SetHeight {screen height}] - Interact with the game screen"
        };

        private void OnEnable()
        {
            Main.a_OnFrame += ConsoleUpdate;
            tmp_InputField.onEndEdit.AddListener(DoConsoleCommand);
        }
        private void OnDestroy()
        {
            Main.a_OnFrame -= ConsoleUpdate;
            tmp_InputField.onEndEdit.RemoveListener(DoConsoleCommand);
        }

        private void Start()
        {
            ToggleConsole(false);
            for (int i = 0; i < l_CommandList.Count; ++i)
            {
                tmp_CommandList.text += $"{l_CommandList[i]}\n";
            }
        }

        void ConsoleUpdate()
        {
            if (InputSystem.GetInput(s_ConsoleBinding, InputSystem.KeyType.Down) && !b_InConsole)
            {
                ToggleConsole(!b_InConsole);
            }

            if(b_InConsole)
            {
                if(InputSystem.GetInput("UpArrow", InputSystem.KeyType.Down))
                {
                    tmp_InputField.text = s_LastCommand;
                }
            }
        }
        void ToggleConsole(bool active)
        {
            b_InConsole = active;
            tmp_InputField.gameObject.SetActive(active);

            Cursor.Instance.SetCursor(b_InConsole ? "Ibeam" : "Arrow", new() { B_Visible = true, u_LockMode = CursorLockMode.None });
            if (active) tmp_InputField.ActivateInputField();
        }

        void DoConsoleCommand(string s_CurrentCommand)
        {
            s_LastCommand = s_CurrentCommand;
            tmp_InputField.text = "";
            ToggleConsole(false);
            Debug.Log($"Console command: {s_CurrentCommand}");

            string[] parts = s_CurrentCommand.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0) return;  

            string command = parts[0];
            object value = parts.Length > 1 ? parts[1] : null;

            // Evaluate Command
            switch (command.ToLowerInvariant())
            {
                case "hang":
                    {
                        Main.Instance.Hang();
                        break;
                    }
                case "open":
                    {
                        if (value == null)
                        {
                            Debug.LogError("Open command requires a scene name.");
                            return;
                        }

                        if (value.ToString().ToLowerInvariant() == "self")
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        }
                        else
                        {
                            SceneManager.LoadScene(value.ToString());
                        }
                        break;
                    }
                case "playtimelimiter":
                    {
                        if (value == null)
                        {
                            Debug.LogError("PlaytimeLimiter command requires a subcommand.");
                            return;
                        }

                        if (value.ToString().ToLowerInvariant() == "warn")
                        {
                            PlaytimeLimiter.a_SetTimer(PlaytimeLimiter.f_WarningTime);
                        }
                        else
                        {
                            PlaytimeLimiter.a_SetTimer((float)System.Convert.ToDecimal(value));
                        }
                        break;
                    }
                case "profiler":
                    {
                        DevWidget.a_ToggleProfiler?.Invoke();
                        break;
                    }
                case "discord":
                    {
                        if (value == null)
                        {
                            Debug.LogError("Discord command requires a subcommand.");
                            return;
                        }

                        if (value.ToString().ToLowerInvariant() == "disconnect")
                        {
                            DiscordController.a_Disconnect?.Invoke();
                        }
                        break;
                    }
                case "soundsystem":
                    {
                        if (value == null)
                        {
                            Debug.LogError("Sound System command requires a subcommand.");
                            return;
                        }


                        switch (value.ToString().ToLowerInvariant())
                        {
                            case "play":
                                {
                                    if (parts.Length == 3)
                                    {
                                        Audio.Instance.PlaySound(parts[2]?.ToString());
                                    }
                                    else
                                    {
                                        Debug.LogError("Sound System 'Play' command requires another subcommand.");
                                        return;
                                    }
                                    break;
                                }
                            case "stop":
                                {
                                    Audio.Instance.StopAllSounds();
                                    break;
                                }
                            case "toggle":
                                {
                                    Audio.Instance.ToggleAudioChannels();
                                    break;
                                }
                        };
                        break;
                    }
                case "screen":
                    {
                        if (value == null)
                        {
                            Debug.LogError("Screen command requires a subcommand.");
                            return;
                        }

                        switch (value.ToString().ToLowerInvariant())
                        {
                            case "fullscreen":
                                {
                                    Screen.fullScreen = !Screen.fullScreen;
                                    break;
                                }
                            case "setwidth":
                                {
                                    Screen.SetResolution(Convert.ToInt32(parts[2]), Screen.height, Screen.fullScreen);
                                    break;
                                }
                            case "setheight":
                                {
                                    Screen.SetResolution(Screen.width, Convert.ToInt32(parts[2]), Screen.fullScreen);
                                    break;
                                }
                        }
                        break;
                    }
            }
        }
    }
}
