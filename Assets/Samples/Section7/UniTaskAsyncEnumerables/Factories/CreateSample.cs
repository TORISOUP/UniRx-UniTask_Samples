using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Factories
{
    public class CreateSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            await CountDownAsync(10, TimeSpan.FromSeconds(1))
                .ForEachAsync(x => Debug.Log(x));
        }

        // 指定の整数がゼロになるまで一定間隔でカウントダウンする
        private IUniTaskAsyncEnumerable<int> CountDownAsync(int startCount, TimeSpan timeSpan)
        {
            return UniTaskAsyncEnumerable.Create<int>(async (writer, token) =>
            {
                var currentCount = startCount;
                while (currentCount >= 0)
                {
                    // writer.YieldAsync を使うと UniTaskAsyncEnumerable に値が書き込まれる
                    await writer.YieldAsync(currentCount--);
                    await UniTask.Delay(timeSpan, cancellationToken: token);
                }
            });
        }
    }
}