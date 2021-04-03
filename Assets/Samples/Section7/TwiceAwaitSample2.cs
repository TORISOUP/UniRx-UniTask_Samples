using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples.Section7
{
    public class TwiceAwaitSample2 : MonoBehaviour
    {
        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            DoAsync(token).Forget();
        }

        private async UniTaskVoid DoAsync(CancellationToken token)
        {
            try
            {
                // HTTP GETを行い結果をキャッシュするUniTask
                var uniTask = GetAsync("https://unity.com/ja", token);

                // Preserve()を呼び出すことで、
                // 何回でもawait可能なUniTaskに変換できる
                var reusable = uniTask.Preserve();

                await reusable;
                await reusable;
            }
            catch (InvalidOperationException e)
            {
                Debug.LogException(e);
            }
        }

        /// <summary>
        /// HTTP GETを行う
        /// </summary>
        private async UniTask<string> GetAsync(string uri, CancellationToken token)
        {
            using (var uwr = UnityWebRequest.Get(uri))
            {
                await uwr.SendWebRequest().ToUniTask(cancellationToken: token);
                return uwr.downloadHandler.text;
            }
        }
    }
}