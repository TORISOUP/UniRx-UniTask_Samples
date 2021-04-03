using System;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples.Section4.ErrorHandlers
{
    public class OnErrorRetrySample2 : MonoBehaviour
    {
        private void Start()
        {
            var url = "https://unity3d.com/jp";

            Observable.Defer(() => FetchAsync(url).ToObservable())
                // OnErrorメッセージが発行されたら、
                // 「ログを出力」してから
                // 「3秒待ってからリトライ」を
                // 「合計3回まで」繰り返す
                .OnErrorRetry(
                    onError: (Exception ex) => Debug.LogWarning(ex.Message),
                    delay: TimeSpan.FromSeconds(3),
                    retryCount: 3)
                .Subscribe(
                    x => Debug.Log(x),
                    ex => Debug.LogError(ex),
                    () => Debug.Log("OnCompleted")
                );
        }

        /// <summary>
        /// HTTP通信をUnityWebRequestで行う
        /// </summary>
        private async UniTask<string> FetchAsync(string uri)
        {
            Debug.Log("Do");
            using (var uwr = UnityWebRequest.Get(uri))
            {
                // UniTaskを導入した場合はawaitができる
                await uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    throw new Exception($"Error>{uwr.error}");
                }

                return uwr.downloadHandler.text;
            }
        }
    }
}