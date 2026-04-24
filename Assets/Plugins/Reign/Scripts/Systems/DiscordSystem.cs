using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
using Reign.Generics;
using System;

namespace Reign.Systems
{
    [Serializable]
    public struct DiscordSystemData
    {
        public string details;
        public string state;
        public string largeImage;
        public string largeImageText;
        public string smallImage;
        public string smallImageText;
        public long startUnixTimestamp;
        public long endUnixTimestamp;
    }

    public class DiscordSystem : System<DiscordSystem>
    {
        [SerializeField] private DiscordSystemData defaultDiscordSystemData;

        public bool canConnect { get; private set; } = false;
        public bool isConnected { get; private set; } = false;
        private DiscordSystemData currentDiscordSystemSettings;
        public Discord.Discord discord { get; private set; } = null;
        private ActivityManager manager;

        /// <summary>
        /// Set current Discord RPC data
        /// </summary>
        /// <param name="data">Data to set</param>
        internal void SetData(DiscordSystemData data)
        {
            if (!data.Equals(currentDiscordSystemSettings))
            {
                currentDiscordSystemSettings = data;
                UpdateStatus();
            }
        }

        /// <summary>
        /// Get current Discord RPC data
        /// </summary>
        /// <returns>DiscordSystemData</returns>
        internal DiscordSystemData GetCurrent()
        {
            return currentDiscordSystemSettings;
        }

        /// <summary>
        /// Set current data's start timestamp to the current unix time 
        /// </summary>
        internal void ResetTimestamp()
        {
            currentDiscordSystemSettings.startUnixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        private void Start()
        {
            canConnect = GameCertificates.DISCORD_ENABLED;

            if (defaultDiscordSystemData.startUnixTimestamp == 0)
            {
                ResetTimestamp();
            }

            SetData(defaultDiscordSystemData);

            AttemptConnection();
        }

        private void OnDestroy()
        {
            Disconnect();
        }

        private void Update()
        {
            canConnect = !isConnected;
            AttemptCallbacks();
        }

        private void Disconnect()
        {
            if (discord != null)
            {
                discord.Dispose();
                discord = null;
            }
        }

        private void AttemptConnection()
        {
            if (!canConnect)
            {
                Disconnect();
                return;
            }

            if (discord == null)
            {
                try
                {
                    discord = new Discord.Discord(GameCertificates.DISCORD_APP_ID, (ulong)CreateFlags.NoRequireDiscord);
                    UpdateStatus();
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Discord connection failed: {e.Message}");
                    Disconnect();
                }
            }
        }

        private void AttemptCallbacks()
        {
            if (discord == null) return;

            try
            {
                discord.RunCallbacks();
            }
            catch
            {
                Disconnect();
            }
        }

        private void UpdateStatus()
        {
            if (discord == null) return;

            try
            {
                manager ??= discord.GetActivityManager();

                Activity activity = new()
                {
                    Details = currentDiscordSystemSettings.details,
                    State = currentDiscordSystemSettings.state,
                    Assets =
                    {
                        LargeImage = currentDiscordSystemSettings.largeImage,
                        LargeText = currentDiscordSystemSettings.largeImageText,
                        SmallImage = currentDiscordSystemSettings.smallImage,
                        SmallText = currentDiscordSystemSettings.smallImageText,
                    },

                    Timestamps =
                    {
                        Start = currentDiscordSystemSettings.startUnixTimestamp,
                        End = currentDiscordSystemSettings.endUnixTimestamp
                    },
                };

                manager.UpdateActivity(activity, result =>
                {
                    isConnected = result == Result.Ok;
                });
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"Discord RPC failed to update: {exception.Message}");
            }
        }
    }
}
