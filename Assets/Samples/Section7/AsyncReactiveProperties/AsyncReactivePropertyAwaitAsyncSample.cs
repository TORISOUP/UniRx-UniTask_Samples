using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.AsyncReactiveProperties
{
    public class AsyncReactivePropertyAwaitAsyncSample : MonoBehaviour
    {
        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            // AsyncReactiveProperty生成
            var asyncReactiveProperty = new AsyncReactiveProperty<string>("Initialize!");

            // 待受開始
            WaitForAsync(asyncReactiveProperty, token).Forget();

            // 値を設定
            asyncReactiveProperty.Value = "Hello!";
            asyncReactiveProperty.Value = "World!";
            asyncReactiveProperty.Value = "Thank you!";

            asyncReactiveProperty.Dispose();
        }

        private async UniTaskVoid WaitForAsync(
            IReadOnlyAsyncReactiveProperty<string> asyncReadOnlyReactiveProperty,
            CancellationToken token)
        {
            await asyncReadOnlyReactiveProperty
                // 1秒待ってから MoveNextAsync() を実行する
                .ForEachAwaitWithCancellationAsync(async (x, ct) =>
                {
                    Debug.Log(x);
                    await UniTask.Delay(1000, cancellationToken: ct);
                }, token);
        }
    }
}