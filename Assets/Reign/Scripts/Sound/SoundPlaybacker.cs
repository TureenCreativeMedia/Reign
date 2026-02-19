using UnityEngine;

namespace reign
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlaybacker : MonoBehaviour
    {
        [SerializeField] bool bool_AccountForTimeScale = true;
        [SerializeField] string string_AutoplaySound;
        public AudioSource AudioSource_Source { get; private set; }
        void OnEnable()
        {
            OriginSystem.Action_OnAwake += Initiate;
        }
        void OnDisable()
        {
            OriginSystem.Action_OnAwake += Initiate;
        }
        public virtual void Initiate()
        {
            AudioSource_Source = GetComponent<AudioSource>();

            if(string_AutoplaySound != null) PlayOneShot(string_AutoplaySound);
        }
        public virtual void PlayOneShot(string NAME, float VOLUME = 0.6f)
        {
            AudioSource_Source.PlayOneShot(MasterSystem.Instance._SoundSystem.GetSound(NAME), VOLUME);
        }
        public virtual void Play(string NAME, float VOLUME, bool LOOP, float PITCH = 1.0f)
        {
            AudioSource_Source.clip = MasterSystem.Instance._SoundSystem.GetSound(NAME);
            AudioSource_Source.volume = VOLUME;
            AudioSource_Source.loop = LOOP;
            AudioSource_Source.pitch = bool_AccountForTimeScale ? PITCH * MasterSystem.Instance._TimeSystem.float_TimeScale : PITCH;

            AudioSource_Source.Play();
        }
    }
}
