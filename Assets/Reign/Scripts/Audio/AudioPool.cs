using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    [CreateAssetMenu(fileName = "Reign/Audio Pool", menuName = "Reign/Audio Pool", order = 99)]
    public class AudioPool : ScriptableObject
    {
        public List<AudioClip> u_AudioClips;

        public AudioClip GetClipByName(string clipName)
        {
            for (int i = 0; i < u_AudioClips.Count; ++i)
            {
                if (u_AudioClips[i].name == clipName)
                {
                    return u_AudioClips[i];
                }
            }
            return null;
        }
    }
}
