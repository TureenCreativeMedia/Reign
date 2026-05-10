using System.Threading.Tasks;
using Reign.Systems;
using UnityEngine;

namespace Reign.Essentials
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        private AudioSource source;

        private void Awake()
        {
            TryGetComponent(out source);
        }

        public void Play(string clip, int index, bool loop)
        {
            AudioSystem.Instance.Play(source, clip, transform.position, index, loop);
        }

        public async Task MovePitch(float target, float speed = 1.0f)
        {
            while (Mathf.Abs(source.pitch - target) > 0.001f)
            {
                source.pitch = Mathf.MoveTowards(source.pitch, target, speed * Time.deltaTime);
                await Task.Yield();
            }

            source.pitch = target;
        }

        public void Stop()
        {
            source.Stop();
        }
    }
}
