using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Reign.ScriptableObjects.Audio
{
    [CreateAssetMenu(fileName = "AudioPool", menuName = "Reign/Audio Pool")]
    public class AudioPool : ScriptableObject
    {
        [AllowNesting] [Label("Groups")] public List<AudioPoolGroup> List_Groups;
    }
}
