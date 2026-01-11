using Discord;
using System;
using System.Collections;
using UnityEngine;

namespace reign
{
    public class DiscordController : MonoBehaviour
    {
        public static Action a_Disconnect;

        public bool b_Ignore = false;
        public long l_AppID;
        [Space]
        public string s_Details = "Description";
        public string s_State = "In-Game State";
        [Space]
        public string s_PreviewImage = "Game_Logo";
        public string s_PreviewImageText = "Hover_Logo_Text";

        public static bool b_Connected;
        public Discord.Discord e_Discord;

        // Edit RPC
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
            if (b_Ignore || !App.u_localdata.discord)
            {
                Disconnect();
                return;
            }

            e_Discord = new Discord.Discord(l_AppID, (System.UInt64)CreateFlags.NoRequireDiscord);

            if (e_Discord == null)
            {
                Disconnect();
            }
            else
            {
                UpdateStatus();
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
                        Start = Main.Instance.l_AppStartUnixTimestamp,
                    }
                };

                activityManager.UpdateActivity(activity, (result) =>
                {
                    b_Connected = (result == 0 && !b_Ignore);
                    if (result != 0)
                    {
                        Debug.LogError($"Failed to update Discord status: {result}");
                    }
                });
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Discord RPC status update failed: {ex.Message}");
                Disconnect();
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
