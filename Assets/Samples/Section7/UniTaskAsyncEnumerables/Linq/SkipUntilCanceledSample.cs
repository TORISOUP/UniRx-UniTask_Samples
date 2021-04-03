using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Linq
{
    public class SkipUntilCanceledSample : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        private void Start()
        {
            var targetToken = _target.GetCancellationTokenOnDestroy();

            UniTaskAsyncEnumerable.EveryUpdate()
                // 対象がキャンセル状態になったら値が取得できるようになる
                .SkipUntilCanceled(targetToken)
                .ForEachAsync(
                    _ => Debug.Log(Time.frameCount),
                    this.GetCancellationTokenOnDestroy());
        }
    }
}