using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.HotConverters
{
    public class ShareSample : MonoBehaviour
    {
        private void Start()
        {
            // 1秒ごとにメッセージを発行するタイマ
            var original = Observable.Interval(TimeSpan.FromSeconds(1))
                .TakeUntilDestroy(this);

            // RefCountによって、Observerが1つ以上ある場合は自動的にConnect()される
            // Observerがゼロになったら自動Dispose()
            var published = original.Share();

            // Connect()が自動実行される
            published.Subscribe().AddTo(this);
            published.Subscribe().AddTo(this);
            published.Subscribe().AddTo(this);
        }
    }
}