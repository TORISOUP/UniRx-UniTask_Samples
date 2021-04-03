using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Factories
{
    public class RangeSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            await UniTaskAsyncEnumerable
                .Range(0, 5)
                .ForEachAsync(x => Debug.Log(x));
            
            Debug.Log("Done");
        }
    }
}