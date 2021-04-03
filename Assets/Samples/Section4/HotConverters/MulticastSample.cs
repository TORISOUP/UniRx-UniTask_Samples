using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.HotConverters
{
    public class MulticastSample : MonoBehaviour
    {
        private void Start()
        {
            // 1秒ごとにメッセージを発行するタイマ
            // Observable.IntervalはSubscribeされるたびに新しいObservableを生成してしまう
            var original = 
                Observable.Interval(TimeSpan.FromSeconds(1))
                    .TakeUntilDestroy(this);

            // "Subject"を用いてHot変換用のConnectableObservableに変換する
            IConnectableObservable<long> multicasted
                = original.Multicast(new Subject<long>());

            // original.Subscribe(subject) に相当する処理
            var connectDisposable = multicasted.Connect();

            // Multicastで指定したSubjectに対してSubscribeするのと同じ
            multicasted.Subscribe().AddTo(this);
            multicasted.Subscribe().AddTo(this);
            multicasted.Subscribe().AddTo(this);

            // Hot変換終了
            connectDisposable.Dispose();
        }
    }
}