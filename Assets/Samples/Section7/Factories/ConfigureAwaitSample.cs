using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Factories
{
    public class ConfigureAwaitSample : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            WaitForButton(this.GetCancellationTokenOnDestroy()).Forget();
        }

        /// <summary>
        /// ボタンが押されたらジャンプする
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async UniTaskVoid WaitForButton(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                // Update()のタイミングでボタンの入力を待ったあとに
                // FixedUpdate()タイミングにスイッチする
                await UniTask
                    .WaitUntil(
                        predicate: () => Input.GetButtonDown("Jump"),
                        timing: PlayerLoopTiming.Update,
                        cancellationToken: token);

                _rigidbody.AddForce(Vector3.up * 100.0f, ForceMode.Acceleration);
            }
        }
    }
}