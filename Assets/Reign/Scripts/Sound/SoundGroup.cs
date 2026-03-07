using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    [CreateAssetMenu(fileName = "Reign/Sound/Sound Group", menuName = "Reign/Sound/Sound Group")]
	public class SoundGroup : ScriptableObject
	{
		public List<Sound> List_Sounds;
	}
}
