using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Reign.Generics.Audio
{
    [CreateAssetMenu(fileName = "New Audio Pool", menuName = "Reign/New Audio Pool")]
    internal class AudioPool : ScriptableObject
    {
        [AllowNesting] public List<AudioPoolGroup> audioGroups;
    }
}