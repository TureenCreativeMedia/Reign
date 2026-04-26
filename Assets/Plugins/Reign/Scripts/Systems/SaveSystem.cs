#pragma warning disable CS0162 // Unreachable code detected

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using System.Threading.Tasks;
using Reign.Generics;
using Reign.Generics.Input;
using Reign.Generics.Saving;
using Reign.Generics.Visuals;
using Reign.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Reign.Systems
{
    [System.Serializable]
    public sealed class GameData
    {
        public ScreenSettings screenResolution;
        public InputMap inputMap;

        public float masterAudioVolume;

        public GameData()
        {
            screenResolution = new ScreenSettings(1280, 720, false, true, true);

            masterAudioVolume = -2.0f; // in Decibels

            inputMap = new InputMap()
            {
                inputs = new Dictionary<string, KeyCode[]>()
                {
                    {"any", new KeyCode[] { KeyCode.None }}
                }
            };
        }
    }

    [DefaultExecutionOrder(-1)]
    public sealed class SaveSystem : System<SaveSystem>
    {
        private GameData gameData;
        private SaveFileHandler saveFileHandler = new();
        private List<IDataHandler> dataHandlers;

        /// <summary>
        /// Return a list of the present data handlers in the scene
        /// </summary>
        /// <returns></returns>
        internal static List<IDataHandler> GetDataHandlers()
        {
            // Start from MonoBehaviour and not ReignMonoBehaviour because it inherits MonoBehaviour

            var scriptList = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataHandler>().ToList();
            return scriptList;
        }

        private void RefreshHandlers()
        {
            dataHandlers = GetDataHandlers();
        }

        private void LoadHandlers()
        {
            foreach (var handler in dataHandlers)
            {
                handler.LoadData(gameData);
            }
        }

        private void SaveHandlers()
        {
            foreach (var handler in dataHandlers)
            {
                handler.SaveData(ref gameData);
            }
        }

        /// <summary>
        /// Load game data (asynchronously) in every present data handler
        /// </summary>
        /// <returns></returns>
        public async Task LoadGameData()
        {
            if (!GameCertificates.SAVE_SYSTEM_ENABLED)
            {
                Debug.Log("Data tried to load, but SAVE_SYSTEM_ENABLED flag is false");
                return;
            }

            gameData = await saveFileHandler.LoadAsync();

            // Fallback
            gameData ??= new GameData();

            LoadHandlers();

            Debug.Log("Loaded data successfully");
        }

        /// <summary>
        /// Save game data (asynchronously) to every present data handler
        /// </summary>
        /// <returns></returns>
        public async Task SaveGameDataAsync()
        {
            if (!GameCertificates.SAVE_SYSTEM_ENABLED)
            {
                Debug.Log("Data tried to save, but SAVE_SYSTEM_ENABLED flag is false");
                return;
            }

            // Save to all handlers
            SaveHandlers();

            await saveFileHandler.SaveAsync(gameData);

            Debug.Log("Saved data successfully");
        }

        /// <summary>
        /// Save game data (synchronously) to every present data handler
        /// </summary>
        /// <returns></returns>
        public void SaveGameDataSync()
        {
            if (!GameCertificates.SAVE_SYSTEM_ENABLED)
            {
                Debug.Log("Data tried to save, but SAVE_SYSTEM_ENABLED flag is false");
                return;
            }

            // Save to all handlers
            SaveHandlers();

            saveFileHandler.SaveSync(gameData);

            Debug.Log("Saved data successfully");
        }

        // Runtime
        private async Task SetupAsync()
        {
            if (!GameCertificates.SAVE_SYSTEM_ENABLED) return;

            RefreshHandlers();

            await LoadGameData();
        }

        private void OnApplicationQuit()
        {
            if (GameCertificates.SAVE_ON_QUIT)
            {
                SaveGameDataSync();
            }
        }

        private void OnEnable()
        {
            // For every scene loaded, setup.
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StartCoroutine(Setup());
        }

        private IEnumerator Setup()
        {
            yield return null;
            RefreshHandlers();
            RunSetup();
        }

        private async void RunSetup()
        {
            try
            {
                await SetupAsync();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}
