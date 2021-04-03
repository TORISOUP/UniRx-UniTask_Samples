using System;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples.Section3.FactoryMethods
{
    public class UnityWebRequestToObservable : MonoBehaviour
    {
        private void Start()
        {
            // UniTask から Observable に変換できる
            FetchAsync("https://unity.com/ja")
                .ToObservable()
                .Subscribe(x => Debug.Log(x));
        }

        /// <summary>
        /// HTTP通信をUnityWebRequestで行う
        /// </summary>
        private async UniTask<string> FetchAsync(string uri)
        {
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