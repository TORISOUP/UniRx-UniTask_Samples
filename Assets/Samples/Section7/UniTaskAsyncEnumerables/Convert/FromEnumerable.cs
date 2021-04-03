using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Convert
{
    public class FromEnumerable : MonoBehaviour
    {
        private void Start()
        {
            var array = new[] {1, 2, 3, 4, 5};

            // IEnumerableから変換
            array
                .ToUniTaskAsyncEnumerable()
                .ForEachAsync(x => Debug.Log(x),
                    this.GetCancellationTokenOnDestroy());
        }
    }
}