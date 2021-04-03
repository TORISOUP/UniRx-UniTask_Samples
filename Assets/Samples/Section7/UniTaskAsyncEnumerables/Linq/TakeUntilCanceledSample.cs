using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Linq
{
    public class TakeUntilCanceledSample : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        private async UniTaskVoid Start()
        {
            var targetToken = _target.GetCancellationTokenOnDestroy();

            await UniTaskAsyncEnumerable.EveryUpdate()
                // 対象がキャンセル状態になったら
                // UniTaskAsyncEnumerableが完了する
                .TakeUntilCanceled(targetToken)
                .ForEachAsync(
                    _ => Debug.Log(Time.frameCount),
                    this.GetCancellationTokenOnDestroy());
            
            Debug.Log("UniTaskAsyncEnumerable is finished.");
        }
    }
}