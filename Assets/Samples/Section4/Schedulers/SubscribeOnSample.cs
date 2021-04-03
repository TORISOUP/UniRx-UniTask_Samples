using System;
using System.IO;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Schedulers
{
    public class SubscribeOnSample : MonoBehaviour
    {
        private void Start()
        {
            // 何もSchedulerを指定しない場合はそのままメインスレッド上で
            // 同期的にファイル読み込みが実行される
            ReadFile("data.txt")
                .Subscribe(x => Debug.Log(x));

            // SubscribeOnで一旦スレッドプールに移動してからSubscribeを実行する
            // そのため Observable.CreateWithState の実行スレッドがスレッドプールへ変更される
            // (ファイルの読み込み処理が非同期になる)
            ReadFile("data.txt")
                .SubscribeOn(Scheduler.ThreadPool)
                // 結果はメインスレッドに戻す
                .ObserveOnMainThread() 
                .Subscribe(x => Debug.Log(x));
        }

        /// <summary>
        /// 指定されたファイルを読み込む
        /// 読み込み処理は同期的に実行するためキャンセルはできない
        /// </summary>
        private IObservable<string> ReadFile(string path)
        {
            return Observable.CreateWithState<string, string>(path, (p, observer) =>
            {
                using (var r = new StreamReader(p))
                {
                    observer.OnNext(r.ReadToEnd());
                }

                observer.OnCompleted();

                return Disposable.Empty;
            });
        }
    }
}