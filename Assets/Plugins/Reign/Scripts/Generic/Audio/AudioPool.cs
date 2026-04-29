using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Reign.Generic.Audio
{
    [CreateAssetMenu(fileName = "New Audio Pool", menuName = "Reign/New Audio Pool")]
    public class AudioPool : ScriptableObject
    {
        [AllowNesting] public List<AudioPoolGroup> audioGroups;
    }
}