using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UniRx;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Convert
{
    public class FromObservable : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            await Observable.Range(0, 10)
                // ToUniTaskAsyncEnumerableしたタイミングで
                // ObservableがSubscribeされる
                .ToUniTaskAsyncEnumerable()
                .ForEachAwaitAsync(async x =>
                {
                    Debug.Log($"Wait {x} seconds.");
                    await UniTask.Delay(TimeSpan.FromSeconds(x));
                }, this.GetCancellationTokenOnDestroy());

            Debug.Log("Done");
        }
    }
}