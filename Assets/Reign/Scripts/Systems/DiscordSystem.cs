using Discord;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace reign
{
    public class DiscordSystem : MonoBehaviour
    {        
        public static Action Action_Disconnect;

        [Header("Settings")]
        public bool bool_Ignore = false;
        [Space]

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
            Action_Disconnect += Disconnect;
            App.Action_HangApplication += Disconnect;
            OriginSystem.Action_OnStart += AttemptConnection;
        }

        private void OnDisable()
        {
            Action_Disconnect -= Disconnect;
            App.Action_HangApplication -= Disconnect;
            OriginSystem.Action_OnStart -= AttemptConnection;
        }
        
        void Disconnect()
        {
            bool_Ignore = true;
            StartCoroutine(DisposeDiscordAfterCallbacks());
            Destroy(this);
        }
        void AttemptConnection()
        {
            long_UnixTimestamp = App.Instance.long_AppUnixTimestamp;
            Discord_Discord = new Discord.Discord(App.Instance.AppData_App.long_DiscordAppID, (System.UInt64)CreateFlags.NoRequireDiscord);

            if (bool_Ignore || !App.Instance.AppData_App.bool_DiscordRPC)
            {
                Disconnect();
                return;
            }

            if (Discord_Discord == null)
            {
                Disconnect();
            }

            AttemptSearch();
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
            }
            catch
            {
                Disconnect();
            }

            UpdateStatus();
        }

        void UpdateStatus()
        {
            if (Discord_Discord == null || bool_Ignore) return;

            try
            {
                var activityManager = Discord_Discord.GetActivityManager();
                var activity = new Activity
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

                activityManager.UpdateActivity(activity, result =>
                {
                    if (result != Result.Ok)
                    {
                        bool_Connected = false;
                    }
                    else
                    {
                        bool_Connected = true;
                    }
                });
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Discord RPC failed to update: {e.Message}");
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