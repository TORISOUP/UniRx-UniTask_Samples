using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples.Section7.Methods
{
    public class SuppressCancellationThrowSample : MonoBehaviour
    {
        private void Start()
        {
            var cancellationToken = this.GetCancellationTokenOnDestroy();

            var uri = "https://unity.com/ja";
            InitializeAsync(uri, cancellationToken).Forget();
        }

        /// <summary>
        /// URIからデータを取得して表示する
        /// </summary>
        private async UniTaskVoid InitializeAsync(string uri, CancellationToken token)
        {
            // (bool, T) が返り値
            var (isCanceled, result) = await GetAsync(uri, token).SuppressCancellationThrow();

            if (!isCanceled)
            {
                Debug.Log(result);
            }
        }

        private async UniTask<string> GetAsync(string uri, CancellationToken token)
        {
            using (var uwr = UnityWebRequest.Get(uri))
            {
                await uwr.SendWebRequest().ToUniTask(cancellationToken: token);

                if (uwr.isHttpError || uwr.isNetworkError)
                {
                    throw new Exception(uwr.error);
                }

                return uwr.downloadHandler.text;
            }
        }
    }
}