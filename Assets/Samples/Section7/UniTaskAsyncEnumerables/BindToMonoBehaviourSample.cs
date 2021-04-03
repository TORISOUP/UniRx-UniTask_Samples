using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables
{
    public class BindToMonoBehaviourSample : MonoBehaviour
    {
        private void Start()
        {
            // 10フレームに1回、DoMethod()を呼びだす
            UniTaskAsyncEnumerable.EveryUpdate()
                .Select((_, i) => i)
                .Where(x => x % 10 == 0)
                .BindTo(
                    this,
                    (behaviour, _) => behaviour.DoMethod(),
                    this.GetCancellationTokenOnDestroy());
        }

        private void DoMethod()
        {
            Debug.Log(Time.frameCount);
        }
    }
}