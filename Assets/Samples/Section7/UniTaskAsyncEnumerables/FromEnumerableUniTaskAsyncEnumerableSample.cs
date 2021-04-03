using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables
{
    public class FromEnumerableUniTaskAsyncEnumerableSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            var array = new[]
            {
                UniTask.Lazy(() => SendMessageAsync(400, "400")),
                UniTask.Lazy(() => SendMessageAsync(200, "200")),
                UniTask.Lazy(() => SendMessageAsync(800, "800"))
            };

            await array
                // UniTaskAsyncEnumerableに変換
                .ToUniTaskAsyncEnumerable()
                .ForEachAwaitAsync(async x =>
                {
                    Debug.Log("await");
                    var s = await x;
                    Debug.Log(s);
                });

            Debug.Log("Done!");
        }

        /// <summary>
        /// 指定したミリ秒後にメッセージを返す
        /// </summary>
        private async UniTask<string> SendMessageAsync(int millSeconds, string message)
        {
            await UniTask.Delay(millSeconds);
            return message;
        }
    }
}