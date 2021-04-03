using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section7.UniTaskAsyncEnumerables.Linq
{
    public class QueueSample : MonoBehaviour
    {
        [SerializeField] private Button _button;

        void Start()
        {
            // Queueを使うことで多段の非同期処理を独立して動かすことができる
            _button.OnClickAsObservable()
                .ToUniTaskAsyncEnumerable()
                .SelectAwait(async (x, i) =>
                {
                    // 1つ目の非同期処理
                    await UniTask.Delay(200);
                    return (x, i);
                })
                .Queue()
                .ForEachAwaitAsync(async (x, i) =>
                {
                    // 2つ目の非同期処理
                    await UniTask.Delay(1000);
                }, this.GetCancellationTokenOnDestroy());
        }
    }
}