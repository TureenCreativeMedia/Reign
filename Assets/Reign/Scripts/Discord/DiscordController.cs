using Discord;
using System.Collections;
using UnityEngine;

namespace reign
{
    public class DiscordController : MonoBehaviour
    {
        public bool b_IgnoreInEditor = true;
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
            Main.a_OnFrame += AttemptSearch;
        }

        private void OnDisable()
        {
            Main.a_OnFrame -= AttemptSearch;
        }

        void Start()
        {
            if (b_IgnoreInEditor)
            {
#if UNITY_EDITOR
                Destroy(this);
                StartCoroutine(DisposeDiscordAfterCallbacks());
                return;
#endif
            }


            e_Discord = new Discord.Discord(l_AppID, (System.UInt64)CreateFlags.NoRequireDiscord);

            if (e_Discord == null)
            {
                Destroy(this);
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
                Destroy(this);
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
                    if (result != Result.Ok)
                    {
                        Debug.LogError($"Failed to update Discord status: {result}");
                    }

                    b_Connected = result == Result.Ok;
                });
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Discord RPC status update failed: {ex.Message}");
                Destroy(this);
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
