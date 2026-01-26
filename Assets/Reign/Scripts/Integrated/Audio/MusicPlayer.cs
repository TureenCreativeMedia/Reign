using UnityEngine;

namespace reign
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] int i_Channel = 4;
        [SerializeField] private string s_MusicName;
        [SerializeField] bool b_AutoPlay;

        private void Start()
        {
            Play();
        }
        public void Play()
        {
            Debug.Log("Played");
            Audio.Instance.Play(i_Channel, s_MusicName, 0.7f, true);    
        }

        public void Stop()
        {
            Audio.Instance.r_Channels[i_Channel - 1].StopChannel();
        }
    }
}
