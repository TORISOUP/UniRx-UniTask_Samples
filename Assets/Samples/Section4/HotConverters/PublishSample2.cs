using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.HotConverters
{
    public class PublishSample2 : MonoBehaviour
    {
        private void Start()
        {
            // 1秒ごとにメッセージを発行するタイマ
            var original = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .TakeUntilDestroy(this);

            // Publishに初期値を指定するとBehaviorSubject相当になる
            var published = original.Publish(-1);

            // Hot変換実行
            var connectDisposable = published.Connect();

            // 複数回Subscribeしても同じObservableのメッセージを購読する
            published.Subscribe().AddTo(this);
            published.Subscribe().AddTo(this);
            published.Subscribe().AddTo(this);

            // Hot変換終了
            connectDisposable.Dispose();
        }
    }
}