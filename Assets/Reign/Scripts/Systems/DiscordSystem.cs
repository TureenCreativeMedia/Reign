using Discord;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace reign
{
    public struct DisconnectDiscordEvent : IEvent { }
    public class DiscordSystem : MonoBehaviour, IUpdatable
    {        
        public static Action Action_Disconnect;

        bool bool_CanConnect = false;
        
        [Header("Assets")]
        public string string_Details = "Description";
        public string string_State = "State";
        public string string_PreviewImage = "Game Preview";
        public string string_PreviewImageText = "Game Preview Text";
        long long_UnixTimestamp = 0;
        long long_EndUnixTimestamp = 0;

        public static bool bool_Connected {get; private set;} = false;
        public Discord.Discord Discord_Discord { get; private set; }
        public void SetEndTimestamp(long endTime)
        {
            long_EndUnixTimestamp = endTime;
        }
        public void SetTimestamp(long newTime)
        {
            long_UnixTimestamp = newTime;
        }
        public void SetTimestamp(long newTime, long newEndTime = 0)
        {
            long_UnixTimestamp = newTime;
            long_EndUnixTimestamp = newEndTime;
        }
        public void SetDescription(string description)
        {
            string_Details = description;
        }
        public void SetState(string state)
        {
            string_State = state;
        }
        public void SetPreview(string preview)
        {
            string_PreviewImage = preview;
        }
        public void SetPreviewImageText(string text)
        {
            string_PreviewImageText = text;
        }
        private void OnEnable()
        {
            bool_CanConnect = true;

            EventBus.Unsubscribe<DisconnectDiscordEvent>(PurposefulDisconnect);
            EventBus.Unsubscribe<OnHangApplication>(HangDisconnect);
            UpdateSystem.Register(this);
        }
        private void OnDisable()
        {
            UpdateSystem.Unregister(this);

            EventBus.Unsubscribe<DisconnectDiscordEvent>(PurposefulDisconnect);
            EventBus.Unsubscribe<OnHangApplication>(HangDisconnect);
            EventBus.Unsubscribe<OnHangApplication>(HangDisconnect);
        }
        void Start()
        {
            AttemptConnection();
        }
        public void Tick(float DELTATIME)
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

            if (Discord_Discord != null)
            {
                return;
            }

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