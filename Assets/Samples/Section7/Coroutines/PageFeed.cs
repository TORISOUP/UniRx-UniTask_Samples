using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section7.Coroutines
{
    /// <summary>
    /// ページ送りのサンプル
    /// 複数枚設定したテクスチャを、ボタンがクリックされるたびに順番にRawImageに表示していく
    /// </summary>
    public class PageFeed : MonoBehaviour
    {
        [SerializeField] private Texture[] _textures;

        [SerializeField] private Button _button;
        [SerializeField] private RawImage _image;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            InitializeAsync(token).Forget();
        }

        private async UniTaskVoid InitializeAsync(CancellationToken token)
        {
            await PageFeedAsync(token);

            // ページ送りが終わったら破棄する
            Destroy(gameObject);
        }

        /// <summary>
        /// ボタンがクリックされるごとにページ送りする
        /// </summary>
        private async UniTask PageFeedAsync(CancellationToken token)
        {
            // uGUI ButtonのクリックイベントのAsyncHandlerを取得
            using (var clickEventHandler = _button.GetAsyncClickEventHandler(token))
            {
                // ボタンがクリックされるごとにテクスチャを設定
                for (int i = 0; i < _textures.Length; i++)
                {
                    _image.texture = _textures[i];
                    await clickEventHandler.OnClickAsync(); // クリック待ち
                }
            }
        }
    }
}