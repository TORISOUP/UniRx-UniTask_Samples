using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Factories
{
    public class NeverSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            await UniTaskAsyncEnumerable
                .Never<int>()
                .ForEachAsync(
                    x => Debug.Log(x),
                    this.GetCancellationTokenOnDestroy());

            // ここには決して到達しない
            Debug.Log("Done");
        }
    }
}