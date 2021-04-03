using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples.Section7.Methods
{
    public class TimeoutWithoutExceptionSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            // 3秒以内に通信が終わらないとタイムアウト
            var (isTimeout, result) = await GetAsync("https://unity.com/ja", token)
                .TimeoutWithoutException(TimeSpan.FromSeconds(3));

            if (isTimeout)
            {
                Debug.LogError("Timeout");
                return;
            }

            Debug.Log(result);
        }

        private async UniTask<string> GetAsync(string url, CancellationToken token)
        {
            using (var uwr = UnityWebRequest.Get(url))
            {
                await uwr.SendWebRequest().WithCancellation(token);
                return uwr.downloadHandler.text;
            }
        }
    }
}