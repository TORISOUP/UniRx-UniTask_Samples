using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Cancellations
{
    /// <summary>
    /// CancellationTokenSourceを特定のGameObjectに紐付ける
    /// </summary>
    public class RegisterRaiseCancelSample : MonoBehaviour
    {
        private Camera _camera;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private void Start()
        {
            // カメラを取得
            _camera = Camera.main;

            // カメラが破棄されたら、Cancel命令を発行する
            _cts.RegisterRaiseCancelOnDestroy(_camera);

            MoveAsync(_cts.Token).Forget();
        }

        private async UniTaskVoid MoveAsync(CancellationToken token)
        {
            // カメラの正面3mに移動しつづける処理
            var cameraTransform = _camera.transform;
            while (!token.IsCancellationRequested)
            {
                var goal = cameraTransform.position + cameraTransform.forward * 3;
                transform.position = Vector3.Lerp(transform.position, goal, 0.5f);
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }
    }
}