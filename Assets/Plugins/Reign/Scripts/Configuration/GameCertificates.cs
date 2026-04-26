namespace Reign.Generics
{
    public class GameCertificates
    {
        public const string GAME_NAME = "My Reign Game";
        public const string VERSION = "1.0";
        internal const bool IS_DEBUG = true;

        public const bool SAVE_SYSTEM_ENABLED = true;
        internal const bool SAVE_ON_QUIT = true;
        internal const string SAVE_FILE_DIRECTORY = "save.REIGN";
        internal const bool SAVE_ENCRYPT = false;
        internal const string SAVE_PASSWORD = "my_reign_game_save_password";
        internal const string SAVE_SALT = "my_reign_game_save_salt";
        internal const int SAVE_ITERATIONS = 2048;

        public const bool DISCORD_ENABLED = true;
        internal const long DISCORD_APP_ID = 1453862071543271508;
    }

    public class ReignServiceDetails
    {
        public const string REIGN_VERSION = "1.0.0.0";
    }
}
