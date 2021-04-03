using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class CreateSample : MonoBehaviour
    {
        private void Start()
        {
            //'A'から始まるアルファベットを一定時間ごとに順番に生成する
            var observable = Observable.Create<char>(observer =>
            {
                // IDisposableとCancellationTokenがくっついたオブジェクト
                // Dispose()されるとキャンセル状態になる
                var disposable = new CancellationDisposable();

                // スレッドプール上で実行する
                Task.Run(async () =>
                {
                    // 'A' - 'Z' までのアルファベットを発行する
                    for (var i = 0; i < 26; i++)
                    {
                        // 1秒待つ
                        await Task.Delay(TimeSpan.FromSeconds(1), disposable.Token);

                        // 文字を発行
                        observer.OnNext((char) ('A' + i));
                    }

                    // すべて完了したのでOnCompletedメッセージを発行する
                    observer.OnCompleted();
                }, disposable.Token);

                // Subscribe()が中断されたら連動して
                // CancellationTokenもキャンセル状態になる
                return disposable;
            });

            // Observableを生成して購読
            observable.Subscribe(
                x => Debug.Log("OnNext:" + x),
                ex => Debug.LogError("OnError:" + ex.Message),
                () => Debug.Log("OnCompleted")
            ).AddTo(this);
        }
    }
}