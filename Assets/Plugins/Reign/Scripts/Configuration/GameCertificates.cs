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
        public const string SAVE_FILE_DIRECTORY = "GameSave.REIGN";

        // Save System - Encryption
        public const bool SAVE_ENCRYPT = true;
        public const string SAVE_PASSWORD = "my_reign_game_save_password"; // Usually a 24 character random encryption strings
        public const string SAVE_SALT = "my_reign_game_save_salt"; // Usually a 20 character random encryption string
        public const int SAVE_ITERATIONS = 4096;
    }

    public class ReignServiceDetails
    {
        public const string REIGN_VERSION = "1.0.0";
    }
}
