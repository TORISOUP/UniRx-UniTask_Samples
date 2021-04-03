using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Cancellations
{
    /// <summary>
    /// OperationCanceledExceptionの使い方
    /// </summary>
    public class MyAsyncMethodWithCancellation : MonoBehaviour
    {
        private void Start()
        {
            var cancellationToken = this.GetCancellationTokenOnDestroy();

            InitializeAsync(@"data.txt", cancellationToken).Forget();
        }

        /// <summary>
        /// URIからデータを取得して表示する
        /// </summary>
        private async UniTaskVoid InitializeAsync(string path, CancellationToken token)
        {
            try
            {
                var result = await ReadFileAsync(path, token);
                Debug.Log(result);
            }
            catch (OperationCanceledException)
            {
                // キャンセルの場合はそのまま素通し
                throw;

                // OperationCanceledException は未処理のままUniTaskSchedulerへと伝達されるが、
                // UniTaskSchedulerはこれを無視してくれるのでログが出ずに済む
            }
            catch (Exception e)
            {
                // それ以外の例外ならここでハンドリングする
                Debug.LogException(e);
            }
        }

        private async UniTask<string> ReadFileAsync(string path, CancellationToken token)
        {
            var result = await UniTask.Run(() => File.ReadAllText(path));

            // キャンセル済みなら OperationCanceledException を発行する
            token.ThrowIfCancellationRequested();
            return result;
        }
    }
}