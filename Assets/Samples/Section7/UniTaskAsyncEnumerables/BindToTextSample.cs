using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Samples.Section7.UniTaskAsyncEnumerables
{
    public class BindToTextSample : MonoBehaviour
    {
        [SerializeField] private Text _text;

        private void Start()
        {
            var uris = new[]
            {
                "https://www.google.com/",
                "https://unity.com/ja",
                "https://github.com/"
            };

            uris.ToUniTaskAsyncEnumerable()
                // URIに対して順番にHTTP GETする
                .SelectAwait(async x => await FetchAsync(x))
                // 終わったらBodyの中身をそのままText.textに上書き
                .BindTo(
                    _text,
                    rebindOnError: true,
                    cancellationToken: this.GetCancellationTokenOnDestroy());
        }


        private async UniTask<string> FetchAsync(string uri)
        {
            using (var uwr = UnityWebRequest.Get(uri))
            {
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