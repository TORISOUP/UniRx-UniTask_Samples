using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples.Section4.ErrorHandlers
{
    public class CatchSample2 : MonoBehaviour
    {
        [SerializeField] private string[] _serverUrls;

        public void Start()
        {
            // 複数のURIを先頭から読み込み、正常に読み込めた時点で終了
            // 失敗した場合は次のURIに切り替えて読み込み直す
            FetchTextDataAsync(_serverUrls)
                .Subscribe(x => { Debug.Log(x); });
        }

        /// <summary>
        /// データをサーバから読み込んで非同期で返す
        /// </summary>
        private IObservable<string> FetchTextDataAsync(string[] uris)
        {
            // URI一覧を変換
            IObservable<string>[] observables = uris
                // この Select はLINQ
                .Select(x => Observable.Defer(() => FetchAsObservable(x)))
                .ToArray();

            // URIを先頭から読み込んでいく
            return observables.Catch();
        }

        /// <summary>
        /// サーバから読み取る
        /// </summary>
        private IObservable<string> FetchAsObservable(string uri)
        {
            return FetchAsync(uri).ToObservable();
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