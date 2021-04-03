using System;
using System.IO;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    class FactoryMethod1 : MonoBehaviour
    {
        void Start()
        {
// ファイルをスレッドプール上で読み込む処理
            var task = Task.Run(() => File.ReadAllText(@"data.txt"));

// Task -> Observable 変換
// このときの実行コンテキストはスレッドプールのまま
            task.ToObservable()
                // 実行コンテキストをメインスレッドに切り替える
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(x =>
                {
                    // ここに到達した時点で実行コンテキストは
                    // メインスレッドに切り替わっている
                    Debug.Log(x);
                });
        }

        private IObservable<int> RandomObservable()
        {
            return Observable.Create<int>(o =>
            {
                o.OnNext(1);
                o.OnCompleted();
                return Disposable.Empty;
            });
        }
    }
}