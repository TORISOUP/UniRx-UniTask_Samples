using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Convert
{
    public class TriggerSample : MonoBehaviour
    {
        private void Start()
        {
            this.GetAsyncCollisionEnterTrigger()
                .ForEachAwaitAsync(async collision =>
                {
                    // 衝突したら名前を表示したあと、1秒間待機する
                    Debug.Log(collision.gameObject.name);

                    // このawaitが終わるまでの間に発火したOnCollisionEnterは
                    // すべて無視される
                    await UniTask.Delay(TimeSpan.FromSeconds(1));
                }, this.GetCancellationTokenOnDestroy());
        }
    }
}