using System.Collections.Generic;
using Reign.Systems;

namespace Reign.Generic
{
    public sealed class GameCertificates
    {
        public const string GAME_NAME = "My Reign Game";
        public const string VERSION = "1.0";
        public static readonly IReadOnlyList<string> AUTHORS = new[] { "" };
        public const bool IS_DEBUG = false;

        public const bool SAVE_SYSTEM_ENABLED = true;
        public const bool SAVE_ON_QUIT = true;
        public const string SAVE_FILE_DIRECTORY = "save.REIGN";
        internal const bool SAVE_ENCRYPT = true;
        internal const string SAVE_PASSWORD = "my_reign_game_save_password";
        internal const string SAVE_SALT = "my_reign_game_save_salt";
        internal const int SAVE_ITERATIONS = 2048;

        public const bool DISCORD_ENABLED = true;
        public const long DISCORD_APP_ID = 1453862071543271508;

        public static readonly DiscordSystemData DEFAULT_DISCORD_RPC_DATA = new()
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
