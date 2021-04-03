using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Factories
{
    public class UniTaskYieldSample : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            JumpAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid JumpAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                // Jumpボタンが押されるまで待機
                while (!Input.GetButtonDown("Jump"))
                {
                    // 1フレーム待機
                    await UniTask.Yield(PlayerLoopTiming.Update, token);
                }

                // FixedUpdateに切り替え
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);

                // ここの処理はFixedUpdateタイミングで実行される
                _rigidbody.AddForce(Vector3.up * 100.0f, ForceMode.Acceleration);

                // Updateタイミングに戻す
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }
    }
}