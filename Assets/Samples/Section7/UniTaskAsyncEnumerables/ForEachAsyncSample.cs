using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables
{
    public class ForEachAsyncSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            // EveryUpdate()は毎フレームのタイミングで完了するUniTaskを返す
            await UniTaskAsyncEnumerable.EveryUpdate()
                .Select((_, x) => x)
                // 5回まで実行する
                .Take(5)
                // ForEachAsyncで待機する
                .ForEachAsync( _ => Debug.Log(Time.frameCount), token);

            Debug.Log("Done!");
        }
    }
}