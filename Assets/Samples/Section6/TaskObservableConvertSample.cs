using System;
using System.Threading.Tasks;
using UniRx;

namespace Samples.Section6
{
    public class TaskObservableConvertSample
    {
        /// <summary>
        /// Task -> Observable
        /// </summary>
        public IObservable<T> ToObservable<T>(Task<T> task)
        {
            return task.ToObservable();
        }

        /// <summary>
        /// Observable -> Task
        /// </summary>
        public Task<T> ToTask<T>(IObservable<T> observable)
        {
            // Observableの長さが1固定ならこれでOK
            return observable.ToTask();
        }

        /// <summary>
        /// Observable -> Task
        /// </summary>
        public Task<T> ToTask2<T>(IObservable<T> observable)
        {
            // もしObservableの長さが1ではない場合は、
            // OnCompletedメッセージが発行される条件を明記しないといけない
            return observable.Take(1).ToTask();
        }

        /// <summary>
        /// askの結果をメインスレッド上で扱うObservableに変換する
        /// </summary>
        public IObservable<T> ToMainThreadObservable<T>(Task<T> task)
        {
            return task.ToObservable(Scheduler.MainThread);
        }
    }
}