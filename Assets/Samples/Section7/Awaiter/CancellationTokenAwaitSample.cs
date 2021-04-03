using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Awaiter
{
    public class CancellationTokenAwaitSample : MonoBehaviour
    {
        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            WaitForCanceledAsync(token).Forget();
        }

        private async UniTaskVoid WaitForCanceledAsync(CancellationToken token)
        {
            await token.WaitUntilCanceled();
            Debug.Log("Canceled!");
        }
    }
}