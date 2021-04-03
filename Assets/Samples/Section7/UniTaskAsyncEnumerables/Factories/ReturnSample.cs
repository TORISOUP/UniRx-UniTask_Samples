using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Factories
{
    public class ReturnSample : MonoBehaviour
    {
        private void Start()
        {
            UniTaskAsyncEnumerable
                .Return(100)
                .ForEachAsync(x => Debug.Log(x));
        }
    }
}