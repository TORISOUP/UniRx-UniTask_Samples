using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Samples.Section7.Awaiter
{
    public class UnityEventAwaitSample : MonoBehaviour
    {
        public UnityEvent UnityEvent;

        private void Start()
        {
            // CancellationToken取得
            var token = this.GetCancellationTokenOnDestroy();

            MoveAsync(UnityEvent, token).Forget();
        }

        /// <summary>
        /// イベントが発火するまで待機する
        /// </summary>
        private async UniTaskVoid MoveAsync(
            UnityEvent unityEvent, CancellationToken token)
        {
            using (var asyncHandler = unityEvent.GetAsyncEventHandler(token))
            {
                // イベントが発火するまで待つ
                await asyncHandler.OnInvokeAsync();
                Debug.Log("Event received!");
            }
        }
    }
}