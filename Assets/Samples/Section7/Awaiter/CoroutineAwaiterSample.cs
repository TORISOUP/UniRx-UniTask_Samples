using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Awaiter
{
    public class CoroutineAwaiterSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            // コルーチンにawaitをつけるだけで、自動的にコルーチンが起動して待受ができる

            // 前→右→後と順番に移動する
            await MoveCoroutine(Vector3.forward * 1.0f, 2);
            await MoveCoroutine(Vector3.right * 2.0f, 1);
            await MoveCoroutine(Vector3.back * 2.0f, 1);

            var cancellationToken = this.GetCancellationTokenOnDestroy();

            // CancellationTokenを指定する場合
            await MoveCoroutine(Vector3.down * 3.0f, 1)
                .WithCancellation(cancellationToken);
        }

        /// <summary>
        /// 指定した速度で、指定した秒数移動するコルーチン
        /// </summary>
        private IEnumerator MoveCoroutine(Vector3 velocity, float seconds)
        {
            var start = Time.time;
            while ((Time.time - start) < seconds)
            {
                transform.position += velocity * Time.deltaTime;
                yield return null;
            }
        }
    }
}