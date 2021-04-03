using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables
{
    public class ForEachAwaitAsyncSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            // EveryUpdate()は毎フレームのタイミングで完了するUniTaskを返す
            await UniTaskAsyncEnumerable.EveryUpdate()
                .Select((_, x) => x)
                // 5回まで実行する
                .Take(5)
                // ForEachAwaitAsyncで待機する
                .ForEachAwaitAsync(async _ =>
                {
                    Debug.Log("before await:" + Time.frameCount);
                    // ForEachAwaitAsyncはここのawaitを待つ
                    await UniTask.DelayFrame(10, cancellationToken: token);
                    Debug.Log("after await:" + Time.frameCount);
                }, token);

            Debug.Log("Done!");
        }
    }
}