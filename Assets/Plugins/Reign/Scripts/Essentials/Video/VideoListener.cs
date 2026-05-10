using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace Reign.Essentials
{
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoListener : MonoBehaviour
    {
        VideoPlayer player;

        void Awake()
        {
            player = GetComponent<VideoPlayer>();
        }

        public async Task AwaitStartPlaying()
        {
            while (!player.isPlaying)
            {
                await Task.Yield();
            }
        }

        public Task AwaitStopPlaying()
        {
            var tcs = new TaskCompletionSource<bool>();

            player.loopPointReached += _ => tcs.SetResult(true);
            return tcs.Task;
        }
    }
}
