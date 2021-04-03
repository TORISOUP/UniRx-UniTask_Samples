using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Factories
{
    public class RepeatSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            await UniTaskAsyncEnumerable
                .Repeat("Hi!", 3)
                .ForEachAsync(x => Debug.Log(x));

            Debug.Log("Done");
        }
    }
}