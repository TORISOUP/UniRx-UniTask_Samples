using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples.Section7.Factories
{
    public class UniTaskWhenAllSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            // 3つまとめて通信して、すべて完了するまで待つ
            var (unity, google, github) = await UniTask.WhenAll(
                GetAsync("https://unity.com/ja", token),
                GetAsync("https://www.google.com/", token),
                GetAsync("https://github.com/", token)
            );

            Debug.Log(unity);
            Debug.Log(google);
            Debug.Log(github);
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