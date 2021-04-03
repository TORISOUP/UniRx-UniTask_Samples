using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Convert
{
    public class FromTask : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            await Task.Run(() => "Done")
                .ToUniTaskAsyncEnumerable()
                .ForEachAsync(x => Debug.Log(x),
                    this.GetCancellationTokenOnDestroy());
        }
    }
}