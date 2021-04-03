using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Factories
{
    public class TimerSample2 : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            // 無限長のUniTaskAsyncEnumerable
            await UniTaskAsyncEnumerable
                .Timer(
                    dueTime: TimeSpan.FromSeconds(1),
                    period: TimeSpan.FromSeconds(0.1))
                .ForEachAsync(_ =>
                {
                    // 初回のみdueTime後
                    // 2回目以降はperiod間隔
                    Debug.Log(Time.time);
                }, this.GetCancellationTokenOnDestroy());
        }
    }
}