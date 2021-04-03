using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Convert
{
    public class FromUniTask : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            await UniTask.Run(() => "Done")
                .ToUniTaskAsyncEnumerable()
                .ForEachAsync(x => Debug.Log(x),
                    this.GetCancellationTokenOnDestroy());
        }
    }
}