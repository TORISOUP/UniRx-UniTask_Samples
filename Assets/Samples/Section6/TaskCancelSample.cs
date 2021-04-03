using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Samples.Section6
{
    public class TaskCancelSample : MonoBehaviour
    {
        private CancellationTokenSource _cancellationTokenSource;

        private void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            // CancellationTokenSourceからCancellationTokenを取得
            DoAsync(_cancellationTokenSource.Token);
        }

        /// <summary>
        /// 1秒おきにログを出すだけの非同期処理
        /// </summary>
        private async Task DoAsync(CancellationToken token)
        {
            while (true)
            {
                // キャンセルされた時点で停止
                await Task.Delay(1000, token);
                Debug.Log("Do!");
            }
        }

        private void OnDestroy()
        {
            // OnDestroyでキャンセル命令を発行
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}