using Discord;
using NaughtyAttributes;
using Reign.Events;
using Reign.Generic;
using Reign.Main;
using System;
using System.Collections;
using UnityEngine;

namespace Reign.Systems.App
{
    public struct DisconnectDiscordEvent : IEvent { }

    [Serializable]
    public struct DiscordData
    {
        [AllowNesting, Label("Details")] public string string_ClonedDetails;
        [AllowNesting, Label("State")] public string string_ClonedState;
        [AllowNesting, Label("Preview Image ID")] public string string_ClonedPreviewImage;
        [AllowNesting, Label("Preview Image Text")] public string string_ClonedPreviewImageText;
        [AllowNesting, Label("Start Unix Timestamp")] public long long_ClonedUnixTimestamp;
        [AllowNesting, Label("End Unix Timestamp")] public long long_ClonedEndUnixTimestamp;
    }

    public class DiscordSystem : Singleton<DiscordSystem>
    {
        [SerializeField, Label("Default Discord RPC Data")] DiscordData DiscordData_Default;

        bool bool_CanConnect = false;

        protected string string_Details = "";
        protected string string_State = "";
        protected string string_PreviewImage = "";
        protected string string_PreviewImageText = "";
        protected long long_UnixTimestamp = 0;
        protected long long_EndUnixTimestamp = 0;

        public static bool bool_Connected { get; private set; } = false;
        public Discord.Discord Discord_Discord { get; private set; } = null;

        public void SetDiscordData(DiscordData DATA)
        {
            string_Details = DATA.string_ClonedDetails;
            string_State = DATA.string_ClonedState;
            string_PreviewImage = DATA.string_ClonedPreviewImage;
            string_PreviewImageText = DATA.string_ClonedPreviewImageText;
            long_UnixTimestamp = DATA.long_ClonedUnixTimestamp;
            long_EndUnixTimestamp = DATA.long_ClonedEndUnixTimestamp;
        }
        private void OnEnable()
        {
            bool_CanConnect = true;

            EventBus.Subscribe<DisconnectDiscordEvent>(PurposefulDisconnect);
            EventBus.Subscribe<OnProgramQuit>(OnProgramQuit);
        }
        private void OnDisable()
        {
            bool_CanConnect = false;

            EventBus.Unsubscribe<DisconnectDiscordEvent>(PurposefulDisconnect);
            EventBus.Unsubscribe<OnProgramQuit>(OnProgramQuit);
        }
        public DiscordData ReturnCurrentData()
        {
            return new DiscordData
            {
                string_ClonedDetails = string_Details,
                string_ClonedState = string_State,
                string_ClonedPreviewImage = string_PreviewImage,
                string_ClonedPreviewImageText = string_PreviewImageText,
                long_ClonedUnixTimestamp = long_UnixTimestamp,
                long_ClonedEndUnixTimestamp = long_EndUnixTimestamp,
            };
        }
        void Start()
        {
            SetDiscordData(DiscordData_Default);
            AttemptConnection();
        }
        void Update()
        {
            AttemptSearch();
        }

        void OnProgramQuit(OnProgramQuit EVENT)
        {
            Disconnect();
        }

        void PurposefulDisconnect(DisconnectDiscordEvent EVENT)
        {
            Disconnect();
        }

        void Disconnect()
        {
            bool_CanConnect = false;
            StartCoroutine(DisposeDiscordAfterCallbacks());
        }

        void AttemptConnection()
        {
            if (!bool_CanConnect || AppSystem.Instance.AppData_Data == null || AppSystem.Instance.AppData_Data.bool_DiscordRPC == false)
            {
                Disconnect();
                return;
            }

            if (Discord_Discord == null)
            {
                try
                {
                    Discord_Discord = new Discord.Discord(AppSystem.Instance.AppData_Data.long_DiscordAppID, (ulong)(CreateFlags.NoRequireDiscord));
                    UpdateStatus();
                }
                catch
                {
                    Disconnect();
                    return;
                }
            }
        }

        void AttemptSearch()
        {
            if (Discord_Discord == null)
            {
                return;
            }

            try
            {
                Discord_Discord.RunCallbacks();
                UpdateStatus();
            }
            catch
            {
                Disconnect();
            }
        }

        void UpdateStatus()
        {
            if (Discord_Discord == null || !bool_CanConnect) return;

            try
            {
                ActivityManager ActivityManager_Discord = Discord_Discord.GetActivityManager();
                Activity Activity_Discord = new Activity
                {
                    Details = string_Details,
                    State = string_State,
                    Assets =
                    {
                        LargeImage = string_PreviewImage,
                        LargeText = string_PreviewImageText
                    },

                    Timestamps =
                    {
                        Start = long_UnixTimestamp,
                        End = long_EndUnixTimestamp
                    }
                };

                ActivityManager_Discord.UpdateActivity(Activity_Discord, RESULT =>
                {
                    if (RESULT != Result.Ok)
                    {
                        bool_Connected = false;
                    }
                    else
                    {
                        bool_Connected = true;
                    }
                });
            }
            catch (Exception E)
            {
                Debug.LogWarning($"Discord RPC failed to update: {E.Message}");
            }
        }

        private IEnumerator DisposeDiscordAfterCallbacks()
        {
            yield return null;
            Discord_Discord?.Dispose();
            Discord_Discord = null;
        }

        private void OnDestroy()
        {
            Discord_Discord?.Dispose();
            Discord_Discord = null;
        }
    }
}