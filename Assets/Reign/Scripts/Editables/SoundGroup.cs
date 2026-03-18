using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    [CreateAssetMenu(menuName = "Reign/Sound/Sound Group")]
	public class SoundGroup : ScriptableObject
	{
		public List<Sound> List_Sounds;
	}
}
