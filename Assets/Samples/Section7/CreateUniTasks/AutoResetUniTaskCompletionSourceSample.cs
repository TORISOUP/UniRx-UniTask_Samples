using System;
using Cysharp.Threading.Tasks;

namespace Samples.Section7.CreateUniTasks
{
    public class AutoResetUniTaskCompletionSourceSample
    {
        public class MyClass<T>
        {
            public Action<T> onResult;
        }

// 対象のAction<T>をUniTask<T>に変換する
        public UniTask<T> ToUniTask<T>(MyClass<T> myClass)
        {
            var autoResetUtc = AutoResetUniTaskCompletionSource<T>.Create();
            myClass.onResult += value => autoResetUtc.TrySetResult(value);
            return autoResetUtc.Task;
        }
    }
}