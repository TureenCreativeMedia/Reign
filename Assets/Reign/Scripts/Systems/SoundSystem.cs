using System;
using UnityEngine;

namespace reign
{
    [Serializable]
    public class Sound
    {
        public string string_KEY;
        public AudioClip AudioClip_ASSET;
    }
    public class SoundSystem : BaseSystem
	{
		public SoundGroup SoundGroup_Group;
        public AudioClip GetSound(string NAME)
        {

            foreach (Sound SOUND in SoundGroup_Group.List_Sounds)
            {
                if (SOUND.string_KEY == NAME)
                {
                    return SOUND.AudioClip_ASSET;
                }
            }

            Logger.Instance.Log(Logger.enum_LogIntensity.Warning, $"Sound by name of '{NAME}' was not found");
            return null;
        }

        public bool SoundExists(string NAME)
        {
            foreach (Sound SOUND in SoundGroup_Group.List_Sounds)
            {
                if (SOUND.string_KEY == NAME) return true;
            }
            return false;
        }
    }
}