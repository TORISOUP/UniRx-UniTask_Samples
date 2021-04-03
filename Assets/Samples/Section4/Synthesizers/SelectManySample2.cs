using System;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples.Section4.Synthesizers
{
    public class SelectManySample2 : MonoBehaviour
    {
        private void Start()
        {
            // URI一覧
            var uris = new[]
            {
                "https://unity3d.com/jp/",
                "https://www.google.co.jp",
                "https://www.bing.com"
            };

            uris.ToObservable()
                .SelectMany(
                    // URIに対して通信する
                    uri => TryGetAsync(uri).ToObservable(),
                    // URIとその通信結果をペアにする
                    (uri, body) => (uri, body)
                ).Subscribe(x =>
                {
                    var (uri, body) = x;
                    Debug.Log($"{uri}への通信結果:{body}");
                });
        }

        // UniTaskを使ってサーバ通信して、その成否を返す
        private async UniTask<bool> TryGetAsync(string uri)
        {
            using (var uwr = UnityWebRequest.Get(uri))
            {
                try
                {
                    await uwr.SendWebRequest();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    return false;
                }
            }
        }
    }
}