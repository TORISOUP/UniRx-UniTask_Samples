using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace Samples.Section7.Awaiter
{
    /// <summary>
    /// カメラの映像を用いてスクリーンショットを撮るサンプル
    /// </summary>
    public class AsyncGPUReadbackAwaitSample : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private RenderTexture _renderTexture;

        private async UniTaskVoid Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            // カメラ初期化
            InitializeCamera();

            // ３秒待つ
            await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: token);

            // スクリーンショット
            await TakeScreenShotAsync(token);
        }

        private void InitializeCamera()
        {
            _renderTexture =
                new RenderTexture(
                    width: 512, 
                    height: 512, 
                    depth: 24, 
                    format: RenderTextureFormat.ARGB32, 
                    readWrite: RenderTextureReadWrite.Default);
            _camera.targetTexture = _renderTexture;
            _camera.enabled = false;
        }

        /// <summary>
        /// カメラ映像をスクリーンショットとして保存する
        /// </summary>
        private async UniTask TakeScreenShotAsync(CancellationToken ct)
        {
            _camera.enabled = true;
            await UniTask.NextFrame(cancellationToken: ct);
            _camera.enabled = false;

            // GPUからのデータ取得を待つ
            // AsyncGPUReadbackRequest 型が戻ってくる
            var req = await AsyncGPUReadback.Request(_renderTexture, 0)
                .WithCancellation(ct);
            var rawByteArray = req.GetData<byte>();

            // RenderTextureの内容をPNGに変換
            var png = ImageConversion.EncodeNativeArrayToPNG(
                rawByteArray,
                _renderTexture.graphicsFormat,
                (uint) _renderTexture.width,
                (uint) _renderTexture.height);

            var bytes = png.ToArray();
            png.Dispose();

            await UniTask.Run(() => File.WriteAllBytes("screenshot.png", bytes));
        }
    }
}