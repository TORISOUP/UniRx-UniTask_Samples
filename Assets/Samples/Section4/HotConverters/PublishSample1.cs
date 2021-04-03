using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.HotConverters
{
    public class PublishSample1 : MonoBehaviour
    {
        private void Start()
        {
            // 1秒ごとにメッセージを発行するタイマ
            var original = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .TakeUntilDestroy(this);

            // Hot変換
            var published = original.Publish();

            // Hot変換実行
            var connectDisposable = published.Connect();

            // 複数回Subscribeしても同じObservableのメッセージを購読する
            published.Subscribe().AddTo(this);
            published.Subscribe().AddTo(this);
            published.Subscribe().AddTo(this);

            connectDisposable.Dispose();
        }
    }
}