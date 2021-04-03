using System;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples.Section4.Synthesizers
{
    public class WhenAllSample : MonoBehaviour
    {
        private void Start()
        {
            var parallel = Observable.WhenAll(
                FetchAsync("https://unity.com/ja").ToObservable(),
                FetchAsync("https://www.google.co.jp").ToObservable(),
                FetchAsync("https://github.com").ToObservable());

            parallel.Subscribe(xs =>
            {
                Debug.Log(xs[0]); // unity3d
                Debug.Log(xs[1]); // google
                Debug.Log(xs[2]); // github
            });
        }

        /// <summary>
        /// UniTaskを使ってサーバ通信
        /// </summary>
        private async UniTask<string> FetchAsync(string url)
        {
            using (var uwr = UnityWebRequest.Get(url))
            {
                await uwr.SendWebRequest();
                if (uwr.isHttpError || uwr.isNetworkError)
                {
                    throw new Exception(uwr.error);
                }

                return uwr.downloadHandler.text;
            }
        }
    }
}