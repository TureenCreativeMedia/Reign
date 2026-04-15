using System;
using System.Collections;
using System.Collections.Generic;
using Reign.Generics;
using Reign.Interfaces;
using Reign.Systems;
using UnityEditor;
using UnityEngine;

namespace Reign.Systems
{
    [Serializable]
    public sealed class GameData
    {
        public ScreenSettings screenResolution = new(1280, 720, false);
    }

    public sealed class SaveSystem : System<SaveSystem>
    {
        private GameData gameData;
        private List<IDataHandler> dataHandlerReferences;
    }
}
