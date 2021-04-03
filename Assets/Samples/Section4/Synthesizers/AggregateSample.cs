using UniRx;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    internal class AggregateSample : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(0, 10)
                .Aggregate(0, (pre, cur) => pre + cur)
                .Subscribe(x => Debug.Log(x));
        }
    }
}