using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.CreateUniTasks
{
    public class CoroutineToUniTask2 : MonoBehaviour
    {
        private void Start()
        {
            // このGameObjectが破棄されたらキャンセルリクエストが発行される
            // CancellationTokenを生成する
            var cancellationToken = this.GetCancellationTokenOnDestroy();

            // コルーチン -> UniTask
            // ToUniTask()を使うことでCancellationTokenが指定できる
            var fromCoroutineUniTask =
                Coroutine()
                    .ToUniTask(cancellationToken: cancellationToken);

            fromCoroutineUniTask.Forget();
        }

        private IEnumerator Coroutine()
        {
            while (true)
            {
                transform.position += Vector3.forward * Time.deltaTime;
                yield return null;
            }
        }
    }
}