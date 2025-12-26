using UnityEngine;

namespace reign
{
	public class SoundTest: MonoBehaviour
	{
		void Start()
		{
			Audio.Instance.r_Channels[0].PlaySound("AudioTest");
		}
	}
}