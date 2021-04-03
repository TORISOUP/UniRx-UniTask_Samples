using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Factories
{
    public class UniTaskWaitUntilCanceledSample : MonoBehaviour
    {
        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            WaitForCancelAsync(token).Forget();
        }

        private async UniTaskVoid WaitForCancelAsync(CancellationToken token)
        {
            // CancellationTokenがキャンセルされたら完了する
            await UniTask.WaitUntilCanceled(token);
            
            Debug.Log("Canceled!");
        }
    }
}