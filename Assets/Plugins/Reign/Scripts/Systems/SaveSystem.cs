using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reign.Generics;
using Reign.Interfaces;
using UnityEngine;

namespace Reign.Systems
{
    [System.Serializable]
    public sealed class GameData
    {
        public ScreenSettings screenResolution = new(1280, 720, false);
    }

    public sealed class SaveSystem : System<SaveSystem>
    {
        private GameData gameData;
        private List<IDataHandler> dataHandlerReferences => GetDataHandlers();

        private List<IDataHandler> GetDataHandlers()
        {
            // Start from MonoBehaviour and not ReignMonoBehaviour because it inherits MonoBehaviour

            var scriptList = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataHandler>().ToList();
            return scriptList;
        }

        public IEnumerator LoadGameData()
        {
            yield return null;
        }
    }
}
