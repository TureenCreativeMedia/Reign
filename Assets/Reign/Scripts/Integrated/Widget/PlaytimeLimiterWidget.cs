using TMPro;
using UnityEngine;

namespace reign
{
    public class PlaytimeLimiterWidget : ReignWidget
    {
        [SerializeField] TMP_Text tmp_WarningText;
        private void OnEnable()
        {
            a_OnCreate += ScreenSetup;    
        }
        private void OnDisable()
        {
            a_OnCreate -= ScreenSetup;
        }

        void ScreenSetup()
        {
            PlaytimeLimiter.b_CanCreate = false;
            tmp_WarningText.text = $"Warning, your playtime has exceeded <color=red>{Extensions.Time.Format(Extensions.Time.FormatType.HoursMinutesSeconds, PlaytimeLimiter.f_WarningTime)}";
        }
        public void ContinuePlaying()
        {
            PlaytimeLimiter.b_CanCreate = true;
            Destroy(gameObject);
        }
        public void CloseGame()
        {
            Main.Instance.Hang();
        }
    }
}
