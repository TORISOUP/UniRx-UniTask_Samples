using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples.Section7.Methods
{
    public class TimeoutSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            try
            {
                // 3秒以内に通信が終わらないとタイムアウト
                var result = await GetAsync("https://unity.com/ja", token)
                    .Timeout(TimeSpan.FromSeconds(3));

                Debug.Log(result);
            }
            catch (TimeoutException e)
            {
                Debug.LogException(e);
            }
        }

        private async UniTask<string> GetAsync(string url, CancellationToken token)
        {
            using (var uwr = UnityWebRequest.Get(url))
            {
                await uwr.SendWebRequest().ToUniTask(cancellationToken: token);
                return uwr.downloadHandler.text;
            }
        }
    }
}