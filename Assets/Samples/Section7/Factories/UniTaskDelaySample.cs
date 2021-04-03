using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Factories
{
    public class UniTaskDelaySample : MonoBehaviour
    {
        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            DelayDestroy(1000, token).Forget();
        }

        /// <summary>
        /// 指定秒数後にGameObjectを破棄する
        /// </summary>
        private async UniTaskVoid DelayDestroy(int millSeconds, CancellationToken token)
        {
            await UniTask.Delay(millSeconds, cancellationToken: token);
            Destroy(gameObject);
        }
    }
}