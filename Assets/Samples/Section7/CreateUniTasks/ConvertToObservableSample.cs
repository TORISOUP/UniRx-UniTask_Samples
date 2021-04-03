using System;
using UniRx;
using Cysharp.Threading.Tasks;

namespace Samples.Section7.CreateUniTasks
{
    public class ConvertToObservableSample
    {
        /// <summary>
        /// UniTask<T> -> Observable<T>
        /// </summary>
        public IObservable<T> ToObservable<T>(UniTask<T> uniTask)
        {
            // SchedulerはMainThreadScheduler相当が自動的に利用される
            return uniTask.ToObservable();
        }

        /// <summary>
        /// UniTask -> IObservable<Unit>
        /// </summary>
        public IObservable<Unit> ToObservable(UniTask uniTask)
        {
            // 非ジェネリックなUniTaskから変換した場合、IObservable<AsyncUnit>型となる
            // Unit型をUniRx側に一致させたい場合は AsUnitObservable() を併用するとよい。

            // SchedulerはMainThreadScheduler相当が自動的に利用される
            return uniTask.ToObservable().AsUnitObservable();
        }
    }
}