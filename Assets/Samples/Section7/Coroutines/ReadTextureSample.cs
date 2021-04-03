using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Samples.Section7.Coroutines
{
    public class ReadTextureSample : MonoBehaviour
    {
        [SerializeField] private RawImage _image;

        private void Start()
        {
            var resourcePath = "/** テクスチャの場所を示すURL **/";
            var cancellationToken = this.GetCancellationTokenOnDestroy();

            InitializeAsync(resourcePath, cancellationToken).Forget();
        }

        /// <summary>
        /// テクスチャを非同期で読み込み、RawImageに設定する
        /// </summary>
        private async UniTaskVoid InitializeAsync(string uri, CancellationToken token)
        {
            try
            {
                var texture = await GetTextureAsync(uri, token);
                _image.texture = texture;
            }
            catch (OperationCanceledException e)
            {
                // キャンセルの場合は何もしない
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        /// <summary>
        /// テクスチャを取得する
        /// コルーチンではなく、async/awaitを使ってUnityWebRequestを待ち受ける
        /// </summary>
        private async UniTask<Texture> GetTextureAsync(string uri, CancellationToken token)
        {
            using (var uwr = UnityWebRequestTexture.GetTexture(uri))
            {
                await uwr.SendWebRequest().ToUniTask(cancellationToken: token);

                if (uwr.isHttpError || uwr.isNetworkError)
                {
                    throw new Exception(uwr.error);
                }

                return DownloadHandlerTexture.GetContent(uwr);
            }
        }
    }
}