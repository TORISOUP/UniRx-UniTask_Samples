using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Cancellations
{
    /// <summary>
    /// GameObject破棄時にCancelされるCancellationTokenを取得する
    /// </summary>
    public class CancelOnDestroySample : MonoBehaviour
    {
        private void Start()
        {
            // Destroy時にCancelされるTokenを取得
            var token = this.GetCancellationTokenOnDestroy();

            // 発行したTokenを非同期処理に渡す
            MoveAsync(token).Forget();
        }

        private async UniTaskVoid MoveAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                transform.position += Vector3.forward * Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }
    }
}