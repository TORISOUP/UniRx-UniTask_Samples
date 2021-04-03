using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Samples.Section6
{
    class SynchronizationContextSample2 : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("MainThread ID:" + GetCurrentThreadId());

            DoAsync();
        }

        private async Task DoAsync()
        {
            Debug.Log("await前:" + GetCurrentThreadId());

            // Taskに対してConfigureAwait(false)を指定すると
            // SynchronizationContextを無視するようにできる
            await Task.Run(() =>
            {
                Debug.Log("Task.Run:" + GetCurrentThreadId());
            }).ConfigureAwait(false);

            Debug.Log("await後:" + GetCurrentThreadId());
        }

        private int GetCurrentThreadId()
        {
            return Thread.CurrentThread.ManagedThreadId;
        }
    }
}