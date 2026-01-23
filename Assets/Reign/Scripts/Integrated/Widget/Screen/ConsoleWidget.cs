using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace reign
{
    public class ContainedCommand
    {
        public string command;
        public object value;
    }

    public class ConsoleWidget : ReignWidget
    {
        public static Action<ContainedCommand> a_OnCommand;
        public static Action<string> a_AddToCommandList;
        [SerializeField] GameObject u_Container;
        [SerializeField] TMP_InputField tmp_InputField;
        [SerializeField] TMP_Text tmp_CommandList;
        [SerializeField] string s_ConsoleBinding = "BackQuote";
        [SerializeField] bool b_InConsole;
        string s_LastCommand;

        readonly List<string> l_CommandList = new()
        {
            "(Up arrow) - last command",
            "Open [{Scene name}, Self] - Load scene",
            "Hang - Close app",
            "PlaytimeLimiter [Warn, {Timer value}] - Interact with the Playtime Limiter",
            "Profiler - Toggle profiler",
            "Discord [Disconnect] - Interact with the Discord Controller",
            "SoundSystem [Stop, Toggle, Play {Clip name}] - Interact with the sound system",
            "Screen [Fullscreen, SetWidth {screen width}, SetHeight {screen height}] - Interact with the game screen",
            "Time [Scale {time scale}, Pause] - Manipulate Reign time",
            "GameObject [Hide {Object name}]",
            "LoadingScreen [Load {Scene name}]",
            "Cursor [Texture {Texture name}, Visible {Bool}, LockMode {None, Confined, Locked}]"
        };

        private void OnEnable()
        {
            Main.a_OnFrame += ConsoleUpdate;
            a_AddToCommandList += AddToCommandList;
            tmp_InputField.onEndEdit.AddListener(DoConsoleCommand);
        }
        private void OnDestroy()
        {
            Main.a_OnFrame -= ConsoleUpdate;
            a_AddToCommandList -= AddToCommandList;
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
        
        public void AddToCommandList(string description)
        {
            tmp_CommandList.text += description;
        }

        void ConsoleUpdate()
        {
            InputSystem.b_InputsEnabled = !b_InConsole;

            if (b_InConsole)
            {
                if (InputSystem.GetInput("UpArrow", InputSystem.KeyType.Down, true))
                {
                    tmp_InputField.text = s_LastCommand;
                }
            }
            else
            {
                if (InputSystem.GetInput(s_ConsoleBinding, InputSystem.KeyType.Down))
                {
                    ToggleConsole(!b_InConsole);
                }
            }
        }

        void ToggleConsole(bool active)
        {
            b_InConsole = active;
            u_Container.SetActive(active);

            if (active)
            {
                tmp_InputField.ActivateInputField();
            }
        }

        void DoConsoleCommand(string s_CurrentCommand)
        {
            s_LastCommand = s_CurrentCommand;
            ToggleConsole(false);

            Debug.Log($"Console command: {s_CurrentCommand}");
            tmp_InputField.text = "";

            string[] parts = s_CurrentCommand.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                return;
            }

            string command = parts[0];
            object value = parts.Length > 1 ? parts[1] : null;

            a_OnCommand?.Invoke(new ContainedCommand { command = command, value = value });

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
                    ProfilerWidget.a_ToggleProfiler?.Invoke();
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
                    }
                    ;
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
                case "time":
                {
                    if (value == null)
                    {
                        Debug.LogError("Time command requires a subcommand.");
                        return;
                    }

                    switch (value.ToString().ToLowerInvariant())
                    {
                        case "scale":
                        {
                            if (parts.Length == 3)
                            {
                                Main.Instance.f_TimeScale = (float)Convert.ToDecimal(parts[2]);
                            }
                            else
                            {
                                Debug.LogError("Time 'Scale' command requires another subcommand.");
                                return;
                            }
                            break;
                        }
                        case "pause":
                        {
                            Main.Instance.f_TimeScale = (Main.Instance.f_TimeScale == 0) ? 1 : 0;
                            break;
                        }
                    }
                    break;
                }
                case "gameobject":
                {
                    switch (value.ToString().ToLowerInvariant())
                    {
                        case "hide":
                        {
                            if (parts.Length == 3)
                            {
                                GameObject u_Object = GameObject.Find(parts[2].ToString());

                                if (u_Object != null && u_Object.name != gameObject.name && u_Object.name != "ConsoleWidgetGroup")
                                {
                                    u_Object.SetActive(!u_Object.activeInHierarchy);
                                }
                            }
                            else
                            {
                                Debug.LogError("GameObject 'hide' command requires a game object name.");
                                return;
                            }
                            break;
                        }
                    }
                    break;
                }
                case "loadingscreen":
                {
                    switch (value.ToString().ToLowerInvariant())
                    {
                        case "load":
                        {
                            if (parts.Length == 3)
                            {
                                LoadingScreenWidget.a_AttemptLoad(new(){ f_LoadDelay = 1, s_SceneName = parts[2].ToString(), b_ReleaseMouse = true });
                            }
                            else
                            {
                                Debug.LogError("LoadingScreen 'load' command requires a scene name.");
                                return;
                            }
                            break;
                        }
                    }
                    break;
                }
                case "cursor":
                {
                    switch (value.ToString().ToLowerInvariant())
                    {
                        case "texture":
                        {
                            if (parts.Length == 3)
                            {
                                Cursor.Instance.SetCursor(parts[2].ToString(), new()
                                {
                                    b_Visible = UnityEngine.Cursor.visible,
                                    u_LockMode = UnityEngine.Cursor.lockState
                                }
                                );
                            }
                            break;
                        }
                        case "visible":
                        {
                            if (parts.Length == 3)
                            {
                                Cursor.Instance.SetCursor(Cursor.Instance.s_CurrentName, new()
                                {
                                    b_Visible = Convert.ToBoolean(parts[2]),
                                    u_LockMode = UnityEngine.Cursor.lockState
                                }
                                );
                            }
                            break;
                        }
                        case "lockmode":
                        {
                            if (parts.Length == 3)
                            {
                                CursorLockMode u_NextLockMode = CursorLockMode.None;
                                switch (parts[2].ToLowerInvariant())
                                {
                                    case "locked":
                                    {
                                        u_NextLockMode = CursorLockMode.Locked;
                                        break;
                                    }
                                    case "confined":
                                    {
                                        u_NextLockMode = CursorLockMode.Confined;
                                        break;
                                    }
                                    case "none":
                                    {
                                        u_NextLockMode = CursorLockMode.None;
                                        break;
                                    }
                                }

                                Cursor.Instance.SetCursor(Cursor.Instance.s_CurrentName, new()
                                {
                                    b_Visible = UnityEngine.Cursor.visible,
                                    u_LockMode = u_NextLockMode
                                });
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}