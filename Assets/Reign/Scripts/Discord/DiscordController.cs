using Discord;
using System;
using System.Collections;
using UnityEngine;

namespace reign
{
    public class DiscordController : MonoBehaviour
    {
        // External disconnection
        public static Action a_Disconnect;

        public bool b_Ignore = false;
        public long l_AppID;
        [Space]
        public string s_Details = "Description";
        public string s_State = "In-Game State";
        [Space]
        public string s_PreviewImage = "Game_Logo";
        public string s_PreviewImageText = "Hover_Logo_Text";
        long l_UnixTimestamp = 0;
        long l_EndUnixTimestamp = 0;

        public static bool b_Connected;
        public Discord.Discord e_Discord;

        // Edit RPC
        public void SetEndTimestamp(long endTime)
        {
            l_EndUnixTimestamp = endTime;
        }
        public void SetTimestamp(long newTime)
        {
            // Version with just start time
            l_UnixTimestamp = newTime;
        }
        public void SetTimestamp(long newTime, long newEndTime = 0)
        {
            // Version with start and end time
            l_UnixTimestamp = newTime;
            l_EndUnixTimestamp = newEndTime;
        }
        public void SetDescription(string description)
        {
            s_Details = description;
        }
        public void SetState(string state)
        {
            s_State = state;
        }
        public void SetPreview(string preview)
        {
            s_PreviewImage = preview;
        }
        public void SetPreviewImageText(string text)
        {
            s_PreviewImageText = text;
        }
        private void OnEnable()
        {
            a_Disconnect += Disconnect;
            Main.a_OnHang += Disconnect;
            Main.a_OnFrame += AttemptSearch;
        }

        private void OnDisable()
        {
            a_Disconnect -= Disconnect;
            Main.a_OnHang -= Disconnect;
            Main.a_OnFrame -= AttemptSearch;
        }
        //
        void Disconnect()
        {
            b_Ignore = true;
            StartCoroutine(DisposeDiscordAfterCallbacks());
            Destroy(this);
        }
        void Start()
        {
            l_UnixTimestamp = Main.Instance.l_AppStartUnixTimestamp;
            e_Discord = new Discord.Discord(l_AppID, (System.UInt64)CreateFlags.NoRequireDiscord);

            if (b_Ignore || !App.u_localdata.discord)
            {
                Disconnect();
                return;
            }

            if (e_Discord == null)
            {
                Disconnect();
            }
        }

        void AttemptSearch()
        {
            if (e_Discord == null) return;

            try
            {
                e_Discord.RunCallbacks();
            }
            catch
            {
                Disconnect();
            }

            UpdateStatus();
        }

        void UpdateStatus()
        {
            if (e_Discord == null || b_Ignore)
                return;

            try
            {
                var activityManager = e_Discord.GetActivityManager();
                var activity = new Activity
                {
                    Details = s_Details,
                    State = s_State,
                    Assets =
                    {
                        LargeImage = s_PreviewImage,
                        LargeText = s_PreviewImageText
                    },

                    Timestamps =
                    {
                        Start = l_UnixTimestamp,
                        End = l_EndUnixTimestamp
                    }
                };

                activityManager.UpdateActivity(activity, result =>
                {
                    if (result != Result.Ok)
                    {
                        b_Connected = false;
                    }
                    else
                    {
                        b_Connected = true;
                    }
                });
            }
            catch (Exception e)
            {
                // Skip update
                Debug.LogWarning($"Discord RPC failed to update: {e.Message}");
            }
        }
        private IEnumerator DisposeDiscordAfterCallbacks()
        {
            yield return null;
            e_Discord?.Dispose();
            e_Discord = null;
        }
        private void OnApplicationQuit()
        {
            StartCoroutine(DisposeDiscordAfterCallbacks());
        }
        private void OnDestroy()
        {
            e_Discord?.Dispose();
            e_Discord = null;
        }
    }
}
