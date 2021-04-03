using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._6._2.Cancell
{
    class CancelSamples : MonoBehaviour
    {
        private void Start()
        {
            DoAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        // 各種ファクトリメソッドやAwaiterにCancellationTokenを指定する例
        private async UniTask DoAsync(CancellationToken cancellationToken)
        {
            // Delayをawait中にキャンセルされた場合は即時awaitを中断して終了する（await後続の処理も実行されない）
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);

            // AsyncOperationのキャンセルはタイミングによって挙動が異なる
            //
            // await開始時点でキャンセル済みだった場合　→　何もしない
            // await中にキャンセルされた場合 → awaitそのものを中断するが、実行中のAsyncOperationは止まらない
            await SceneManager.LoadSceneAsync("NextScene")
                .WithCancellation(cancellationToken);
        }

        private async UniTask MoveAsync(CancellationToken token)
        {
            while (true)
            {
                // キャンセルされていたらOperationCanceledExceptionを発行
                token.ThrowIfCancellationRequested();
                transform.position += Vector3.forward * Time.deltaTime;

                // 1フレーム待機
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }
    }
}