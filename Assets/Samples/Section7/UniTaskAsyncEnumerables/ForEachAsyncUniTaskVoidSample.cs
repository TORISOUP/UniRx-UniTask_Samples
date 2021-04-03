using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables
{
    public class ForEachAsyncUniTaskVoidSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            await UniTaskAsyncEnumerable.EveryUpdate()
                .Select((_, x) => x)
                .Take(5)
                // ForEachAsyncは同期的に次のMoveNextAsync()を呼びだす
                .ForEachAsync(_ =>
                {
                    // 完了を待機する必要がない非同期処理ならば
                    // UniTask.Void が利用できる
                    UniTask.Action(async () =>
                    {
                        Debug.Log("before await:" + Time.frameCount);
                        await UniTask.DelayFrame(10, cancellationToken: token);
                        Debug.Log("after await:" + Time.frameCount);
                    });
                }, token);

            Debug.Log("Done!");
        }
    }
}