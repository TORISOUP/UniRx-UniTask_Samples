using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.AsyncReactiveProperties
{
    public class AsyncReactivePropertySample : MonoBehaviour
    {
        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            // AsyncReactiveProperty生成
            var asyncReactiveProperty = new AsyncReactiveProperty<int>(0);

            // 書き込み
            WriteAsync(asyncReactiveProperty).Forget();

            // 待受開始
            WaitForAsync(asyncReactiveProperty, token).Forget();
        }

        // 時間を空けて書き込み
        private async UniTaskVoid WriteAsync(
            AsyncReactiveProperty<int> asyncReactiveProperty)
        {
            // 値を設定
            asyncReactiveProperty.Value = 1;
            await UniTask.Yield();

            asyncReactiveProperty.Value = 2;
            await UniTask.Yield();

            asyncReactiveProperty.Value = 3;
            await UniTask.Yield();

            asyncReactiveProperty.Value = -1;
            await UniTask.Yield();

            // Dispose()するとこのAsyncReactivePropertyへの
            // すべてのawaitをキャンセルできる
            asyncReactiveProperty.Dispose();
        }

        private async UniTaskVoid WaitForAsync(
            IReadOnlyAsyncReactiveProperty<int> asyncReadOnlyReactiveProperty,
            CancellationToken token)
        {
            // Valueプロパティで現在値を取得可能
            var current = asyncReadOnlyReactiveProperty.Value;
            Debug.Log($"Current:{current}");

            // 現在の値をスキップして、次の値に更新されるまで待つ
            var next = await asyncReadOnlyReactiveProperty.WaitAsync(token);
            Debug.Log($"Next:{next}");

            // IUniTaskAsyncEnumerable<T>として扱える
            // (LINQと組み合わせ可能)
            var result = await asyncReadOnlyReactiveProperty
                // 負数になるまで待つ
                .FirstOrDefaultAsync(x => x < 0, cancellationToken: token);

            Debug.Log($"LINQ:{result}");
        }
    }
}