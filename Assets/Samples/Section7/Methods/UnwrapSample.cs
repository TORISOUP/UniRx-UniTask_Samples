using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Methods
{
    public class UnwrapSample : MonoBehaviour
    {
        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            DoAsync(token).Forget();
        }

        private async UniTaskVoid DoAsync(CancellationToken token)
        {
            // UniTaskが戻ってくる処理を更にUniTask.Runで包む
            // 結果が UniTask<UniTask<string>> なのを Unwrap() で剥く
            var result = await UniTask.Run(() => HeavyMethodAsync(token));

            Debug.Log(result);
        }

        // 何か重い処理をするUniTaskなメソッド
        private async UniTask<string> HeavyMethodAsync(CancellationToken token)
        {
            /*
             * ここにメインスレッドでも重い処理が入ってたり
             */

            return "Succeeded!";
        }
    }
}