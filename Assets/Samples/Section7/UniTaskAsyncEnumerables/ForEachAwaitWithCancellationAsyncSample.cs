using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables
{
    public class ForEachAwaitWithCancellationAsyncSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            // EveryUpdate()は毎フレームのタイミングで完了するUniTaskを返す
            await UniTaskAsyncEnumerable.EveryUpdate()
                .Select((_, x) => x)
                // 5回まで実行する
                .Take(5)
                // ForEachAwaitWithCancellationAsyncは
                // CancellationTokenが与えられる
                .ForEachAwaitWithCancellationAsync(async (_, ct) =>
                {
                    Debug.Log("before await:" + Time.frameCount);
                    await UniTask.DelayFrame(10, cancellationToken: ct);
                    Debug.Log("after await:" + Time.frameCount);
                }, token);

            Debug.Log("Done!");
        }
    }
}