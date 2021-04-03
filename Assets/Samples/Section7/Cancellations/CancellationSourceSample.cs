using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Cancellations
{
    public class CancellationSourceSample : MonoBehaviour
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private void Start()
        {
            // CancellationTokenSourceからCancellationTokenを発行
            var token = _cts.Token;

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

        private void OnDestroy()
        {
            // キャンセル命令発行
            // このCancellationTokenSourceから発行したTokenすべてに
            // キャンセルリクエストが発行される
            _cts.Cancel();

            // 使い終わったので破棄
            _cts.Dispose();
        }
    }
}