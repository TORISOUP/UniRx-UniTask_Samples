using System;
using System.Threading;
using UniRx;
using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Samples.Section1.Async
{
    /// <summary>
    /// 指定のURIからテクスチャをダウンロードして
    /// RawImageに設定する
    /// </summary>
    public class DownloadTextureUniTask : MonoBehaviour
    {
        [SerializeField] private RawImage _rawImage;

        private void Start()
        {
            // このGameObjectに紐付いたCancellationTokenを取得
            var token = this.GetCancellationTokenOnDestroy();

            // テクスチャのセットアップを実行
            SetupTextureAsync(token).Forget();
        }

        private async UniTaskVoid SetupTextureAsync(CancellationToken token)
        {
            try
            {
                var uri = "<表示したい画像へのアドレス>";

                // UniRxのRetryを使いたいので、UniTaskからObservableへ変換する
                var observable = Observable
                    .Defer(() =>
                    {
                        // UniTask -> IObservable
                        return GetTextureAsync(uri, token)
                            .ToObservable();
                    })
                    .Retry(3);

                // Observableもawaitで待受が可能
                var texture = await observable;

                _rawImage.texture = texture;
            }
            catch (Exception e) when (!(e is OperationCanceledException))
            {
                Debug.LogError(e);
            }
        }


        /// <summary>
        /// コルーチンの代わりにasync/awaitを利用する
        /// 結果は UniTask<Texture> になる
        /// </summary>
        private async UniTask<Texture> GetTextureAsync(
            string uri,
            CancellationToken token)
        {
            using (var uwr = UnityWebRequestTexture.GetTexture(uri))
            {
                await uwr.SendWebRequest().WithCancellation(token);
                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    // 失敗時は例外を発行する
                    throw new Exception(uwr.error);
                }

                return ((DownloadHandlerTexture) uwr.downloadHandler).texture;
            }
        }
    }
}