using System.Collections;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Coroutines
{
    public class FromMicroCoroutineSample : MonoBehaviour
    {
        private void Start()
        {
            // FromCoroutineで変換
            Observable
                .FromCoroutine(() => WaitingCoroutine(5))
                .Subscribe();

            // 対象のコルーチンが yield return null しか使っていないなら、
            // より軽量な　FromMicroCoroutine　が利用可能
            Observable
                .FromMicroCoroutine(() => WaitingCoroutine(5))
                .Subscribe();
        }

        // 指定秒数待機するコルーチン
        private IEnumerator WaitingCoroutine(float seconds)
        {
            // あえてWaitForSecondsを利用しない
            var start = Time.time;
            while (Time.time - start <= seconds)
            {
                yield return null;
            }
        }
    }
}