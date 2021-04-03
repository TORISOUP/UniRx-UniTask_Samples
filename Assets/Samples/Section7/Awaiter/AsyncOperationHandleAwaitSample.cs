using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Samples.Section7.Awaiter
{
    public class AsyncOperationHandleAwaitSample : MonoBehaviour
    {
        /// <summary>
        /// 読み込む対象のAssetReference
        /// </summary>
        [SerializeField] AssetReference _target;

        [SerializeField] private RawImage _image;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            InitializeAsync(_target, token).Forget();
        }

        private async UniTaskVoid InitializeAsync(
            AssetReference target,
            CancellationToken token)
        {
            // Addressables.LoadAssetAsyncをawaitで待ち受ける
            var texture = await Addressables.LoadAssetAsync<Texture>(target)
                .WithCancellation(token);

            _image.texture = texture;
        }
    }
}