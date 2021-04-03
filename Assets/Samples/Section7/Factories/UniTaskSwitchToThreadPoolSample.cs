using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Factories
{
    public class UniTaskSwitchToThreadPoolSample : MonoBehaviour
    {
        private void Start()
        {
            TestAsync().Forget();
        }

        private async UniTaskVoid TestAsync()
        {
            Debug.Log(Thread.CurrentThread.ManagedThreadId);

            // 以降の処理をThreadPoolに切り替え
            await UniTask.SwitchToThreadPool();

            Debug.Log(Thread.CurrentThread.ManagedThreadId);

            //以降の処理をUnityメインスレッドに戻す
            await UniTask.SwitchToMainThread();

            Debug.Log(Thread.CurrentThread.ManagedThreadId);
        }
    }
}