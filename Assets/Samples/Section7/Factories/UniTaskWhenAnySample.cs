using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples.Section7.Factories
{
    public class UniTaskWhenAnySample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            // HTTP GETするが、1秒以内に終わらない場合はタイムアウトさせる
            var (isFinished, result) =
                await UniTask.WhenAny(
                    GetAsync("https://unity.com/ja", token),
                    UniTask.Delay(1000, cancellationToken: token)
                );

            if (isFinished)
            {
                Debug.Log(result);
            }
            else
            {
                Debug.Log("Timeout!");
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