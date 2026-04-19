using UnityEngine;

namespace Reign.Generics
{
    public class GameCertificates
    {
        // Game Details
        public const string GAME_NAME = "My Reign Game";
        public const string VERSION = "1.0";

        // Save System
        public const bool SAVE_SYSTEM = true;
        public const string SAVE_FILE_DIRECTORY = "save.REIGN";

        // Save System - Encryption
        public const bool SAVE_ENCRYPT = false;

        // Usually 24 character random encryption strings
        public const string SAVE_PASSWORD = "my_reign_game_save_password";
        public const string SAVE_SALT = "my_reign_game_save_salt";

        public const int SAVE_ITERATIONS = 2048;

        // Discord
        public const bool DISCORD_ENABLED = true;
        public const long DISCORD_APP_ID = 1453862071543271508;
    }

    public class ReignServiceDetails
    {
        public const string REIGN_VERSION = "1.0.0.0";
    }
}
