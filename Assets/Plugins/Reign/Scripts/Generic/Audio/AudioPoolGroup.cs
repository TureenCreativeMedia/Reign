using NaughtyAttributes;
using System;
using System.Collections.Generic;

namespace Reign.Generic.Audio
{
    [Serializable]
    public class AudioPoolGroup
    {
        public string name;
        [AllowNesting] public List<AudioPoolEntry> audioPoolEntries = new();
    }
}