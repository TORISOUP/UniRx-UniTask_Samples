using System;
using System.Collections;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Methods
{
    public class ToCoroutineSample : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(CoroutineSample());
        }

        private IEnumerator CoroutineSample()
        {
            // UniTaskをコルーチンに変換して待機
            yield return UniTask.Delay(TimeSpan.FromMilliseconds(500)).ToCoroutine();

            // UniTaskをコルーチンに変換し、結果のハンドリングを行う
            yield return UniTask
                .Run(() => { return File.ReadAllText(@"data.txt"); })
                .ToCoroutine(resultHandler: Debug.Log, exceptionHandler: ex => Debug.LogException(ex));
        }
    }
}