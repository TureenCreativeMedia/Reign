using System.Collections.Generic;
using Reign.Systems;
using UnityEngine;

namespace Reign.Generic
{
    [CreateAssetMenu(fileName = "Game Certificates", menuName = "Reign/New Game Certificates")]
    public sealed class GameCertificates : ScriptableObject
    {
        [Header("Game Settings")]
        public string GAME_NAME = "My Reign Game";
        public string VERSION = "1.0";
        public List<string> AUTHORS = new();
        public bool IS_DEBUG = false;

        [Header("Save System")]
        public bool SAVE_SYSTEM_ENABLED = true;
        public bool SAVE_ENCRYPT = true;
        public bool SAVE_ON_QUIT = true;
        public string SAVE_FILE_DIRECTORY = "save.REIGN";
        public string SAVE_PASSWORD = "my_reign_game_save_password";
        public string SAVE_SALT = "my_reign_game_save_salt";
        public int SAVE_ITERATIONS = 2048;

        [Header("Discord")]
        public bool DISCORD_ENABLED = true;
        public long DISCORD_APP_ID = 1453862071543271508;

        public DiscordSystemData DEFAULT_DISCORD_RPC_DATA = new()
        {
            details = "",
            state = "",
            largeImage = "",
            largeImageText = "",
            smallImage = "",
            smallImageText = "",
            startUnixTimestamp = 0,
            endUnixTimestamp = 0
        };
    }
}
