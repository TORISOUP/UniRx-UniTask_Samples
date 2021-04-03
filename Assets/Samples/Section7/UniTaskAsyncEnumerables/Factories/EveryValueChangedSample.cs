using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Factories
{
    public class EveryValueChangedSample : MonoBehaviour
    {
        private void Start()
        {
            // 自身の移動を監視する
            UniTaskAsyncEnumerable
                .EveryValueChanged(transform, t => t.position)
                .ForEachAsync(x => Debug.Log(x));
        }
    }
}