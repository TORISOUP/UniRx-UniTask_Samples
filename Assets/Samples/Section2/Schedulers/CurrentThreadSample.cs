using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Schedulers
{
    public class CurrentThreadSample : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("Unity Main Thread ID:" + Thread.CurrentThread.ManagedThreadId);

            var subject = new Subject<Unit>();
            subject.AddTo(this);

            subject
                // OnNextメッセージを現行スレッドにて処理する
                // つまりそのまま素通しするのと変わらない
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(_ =>
                {
                    Debug.Log("Thread Id:" + Thread.CurrentThread.ManagedThreadId);
                });

            // メインスレッドにてOnNext発行
            subject.OnNext(Unit.Default);

            // 別スレッドからOnNextを発行
            Task.Run(() => subject.OnNext(Unit.Default));
        }
    }
}