using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Factories
{
    public class TimerSample1 : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            await UniTaskAsyncEnumerable
                .Timer(TimeSpan.FromSeconds(1))
                .ForEachAsync(_ =>
                {
                    // awaitしてから1秒後に実行される
                    Debug.Log(Time.time);
                }, this.GetCancellationTokenOnDestroy());

            // ForEachAsyncがすべて終了したら到達
            Debug.Log("Done");
        }
    }
}