using System;
using System.Collections.Generic;
using UnityEngine;

namespace reign
{
	[Serializable]
	public class Sound
	{
		public string string_Name;
		public AudioClip AudioClip_Clip;
	}

	[CreateAssetMenu(fileName = "Reign/Sound/Sound Group", menuName = "Reign/Sound/Sound Group")]
	public class SoundGroup : ScriptableObject
	{
		public List<Sound> List_Sounds;
	}
    public class SoundSystem : BaseSystem
	{
		public SoundGroup SoundGroup_Sounds;

		/// <summary>
		/// Find and get sound with the name of given parameter
		/// </summary>
		public AudioClip GetSound(string NAME)
		{
			foreach (Sound SOUND in SoundGroup_Sounds.List_Sounds)
			{
				if (SOUND.string_Name == NAME)
				{
					return SOUND.AudioClip_Clip;
				}
			}
			Logger.Instance.Log(Logger.enum_LogIntensity.Warning, $"Sound by name of '{NAME}' was not found");
			return null;
		}

		public bool SoundExists(string NAME)
		{
			return GetSound(NAME) != null;
		}
	}
}