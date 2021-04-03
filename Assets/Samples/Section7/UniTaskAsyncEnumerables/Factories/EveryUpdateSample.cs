using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Factories
{
    public class EveryUpdateSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            await UniTaskAsyncEnumerable
                .EveryUpdate()
                .ForEachAsync(_ =>
                {
                    // Every update.
                    Debug.Log(Time.frameCount);
                }, this.GetCancellationTokenOnDestroy());
        }
    }
}