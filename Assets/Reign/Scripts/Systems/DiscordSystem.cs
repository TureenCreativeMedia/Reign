using Discord;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace reign
{
    public struct DisconnectDiscordEvent : IEvent { }
    
    public struct DiscordData
    {
        public string string_ClonedDetails;
        public string string_ClonedState;
        public string string_ClonedPreviewImage;
        public string string_ClonedPreviewImageText;
        public long long_ClonedUnixTimestamp;
        public long long_ClonedEndUnixTimestamp;
    }

    public class DiscordSystem : BaseSystem, IUpdatable
    {        
        bool bool_CanConnect = false;
        
        [Header("Assets")]
        [SerializeField] protected string string_Details = "Description";
        [SerializeField] protected string string_State = "State";
        [SerializeField] protected string string_PreviewImage = "Game Preview";
        [SerializeField] protected string string_PreviewImageText = "Game Preview Text";
        [SerializeField] protected long long_UnixTimestamp = 0;
        [SerializeField] protected long long_EndUnixTimestamp = 0;

        public static bool bool_Connected {get; private set;} = false;
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
            EventBus.Subscribe<OnHangApplication>(HangDisconnect);
            UpdateSystem.Register(this);
        }
        private void OnDisable()
        {
            bool_CanConnect = false;

            EventBus.Unsubscribe<DisconnectDiscordEvent>(PurposefulDisconnect);
            EventBus.Unsubscribe<OnHangApplication>(HangDisconnect);
            UpdateSystem.Unregister(this);
        }
        public DiscordData ReturnCurrentData()
        {
            return new DiscordData {
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
            AttemptConnection();
        }
        void IUpdatable.Tick(float DELTATIME)
        {
            AttemptSearch();
        }
        void HangDisconnect(OnHangApplication EVENT)
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
            if (!bool_CanConnect || App.Instance == null || App.Instance.AppData_App == null)
            {
                Disconnect();
                return;
            }

            if (Discord_Discord == null)
            {
                try
                {
                    Discord_Discord = new Discord.Discord(App.Instance.AppData_App.long_DiscordAppID, (ulong)(CreateFlags.NoRequireDiscord));
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
                Logger.Instance.Log(Logger.enum_LogIntensity.Warning, $"Discord RPC failed to update: {E.Message}");
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