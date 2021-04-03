using System;
using System.Collections;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Coroutines
{
    public class FromCoroutineSample2 : MonoBehaviour
    {
        private void Start()
        {
            // CancellationTokenを利用する場合
            Observable
                .FromCoroutine(token => WaitingCoroutine(token))
                .Subscribe(
                    _ => Debug.Log("OnNext"),
                    () => Debug.Log("OnCompleted"))
                .AddTo(this);
        }

        // CancellationTokenをうけとる
        private IEnumerator WaitingCoroutine(CancellationToken token)
        {
            Debug.Log("Coroutine start.");

            // Observableをコルーチンとして待受ける場合、
            // このコルーチンが停止したタイミングで
            // yield returnで待ち受けているObservableも止まってほしい
            // そのためにCancellationTokenを用いる
            yield return Observable
                .Timer(TimeSpan.FromSeconds(3))
                .ToYieldInstruction(token);

            Debug.Log("Coroutine finish.");
        }
    }
}