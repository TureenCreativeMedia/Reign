using NaughtyAttributes;
using System;
using System.Collections.Generic;

namespace Reign.ScriptableObjects.Audio
{
    [Serializable]
    public class AudioPoolGroup
    { 
        [AllowNesting] [Label("Name")] public string string_Name;
        [AllowNesting] [Label("Entries")] public List<AudioPoolEntry> List_AudioPoolEntries;
    }
}
