using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Coroutines
{
    public class UniTaskCoroutineSample : MonoBehaviour
    {
        private void Start()
        {
            // CancellationToken取得
            var cancellationToken = this.GetCancellationTokenOnDestroy();

            // 特定のパターンでオブジェクトを移動させる
            MovePatternAsync(cancellationToken).Forget();
        }

        /// <summary>
        /// 特定のパターンで移動する
        /// </summary>
        private async UniTaskVoid MovePatternAsync(CancellationToken token)
        {
            await MoveAsync(Vector3.up * 2.0f, 1.0f, token);
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            await MoveAsync(Vector3.down * 1.0f, 3.0f, token);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            await MoveAsync(Vector3.right * 4.0f, 2.0f, token);
            await UniTask.Delay(TimeSpan.FromSeconds(2.0f), cancellationToken: token);
        }

        /// <summary>
        /// 指定した速度で指定秒数移動する
        /// </summary>
        private async UniTask MoveAsync(Vector3 velocity, float seconds, CancellationToken token)
        {
            var startTime = Time.time;
            while (Time.time - startTime < seconds)
            {
                transform.position += velocity * Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }
    }
}