using System;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples.Section4.ErrorHandlers
{
    public class RetrySample : MonoBehaviour
    {
        private void Start()
        {
            var url = "https://unity3d.com/jp";

            // Deferで対象のObservableを包まないと、
            // Errorの状態で確定したObservableに対してRetryしてしまう
            Observable.Defer(() => FetchAsync(url).ToObservable())
                .Retry(3) // 3回試して失敗したらOnErrorを流す
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