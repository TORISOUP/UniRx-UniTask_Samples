using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Samples.Section6
{
    public class TaskAndObservableSample : MonoBehaviour
    {
        private void Start()
        {
            // ---Task---

            // メインスレッドに戻すためのTaskScheduler
            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
                {
                    // ThreadPool上で行う処理
                    Thread.Sleep(1000);
                    return "result!";
                })
                .ContinueWith(t =>
                {
                    Debug.Log(t.Result);
                }, taskScheduler); // 実行コンテキストをメインスレッドに戻す




            // ---Observable---

            Observable.Start(() =>
                {
                    // ThreadPool上で行う処理
                    Thread.Sleep(1000);
                    return "result!";
                })
                .ObserveOnMainThread() // 実行コンテキストをメインスレッドに戻す
                .Subscribe(r =>
                {
                    Debug.Log(r);
                });
        }
    }
}