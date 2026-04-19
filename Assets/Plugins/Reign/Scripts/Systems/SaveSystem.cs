using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private List<IDataHandler> GetDataHandlers()
        {
            // Start from MonoBehaviour and not ReignMonoBehaviour because it inherits MonoBehaviour

            var scriptList = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataHandler>().ToList();
            return scriptList;
        }

        private void RefreshHandlers()
        {
            dataHandlers = GetDataHandlers();
        }

        public async Task LoadGameData()
        {
            // Load to all handlers

            gameData = await saveFileHandler.LoadAsync();

            // Fallback
            gameData = new GameData();

            foreach (var handler in dataHandlers)
            {
                handler.LoadData(gameData);
            }

            Debug.Log("Loaded data successfully");
        }

        public async Task SaveGameData()
        {
            // Save to all handlers

            foreach (var handler in dataHandlers)
            {
                handler.SaveData(ref gameData);
            }

            await saveFileHandler.SaveAsync(gameData);

            Debug.Log("Saved data successfully");
        }

        // Runtime
        private async Task SetupAsync()
        {
            RefreshHandlers();
            await LoadGameData();
        }

        private void OnEnable()
        {
            // For every scene loaded, setup.
            SceneManager.activeSceneChanged += OnChangeScene;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnChangeScene;
        }

        private async void OnChangeScene(Scene a, Scene b)
        {
            await SetupAsync();
        }
    }
}
