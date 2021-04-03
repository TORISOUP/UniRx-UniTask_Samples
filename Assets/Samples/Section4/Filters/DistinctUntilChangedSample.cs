using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class DistinctUntilChangedSample : MonoBehaviour
    {
        private void Start()
        {
            var array = new[] {1, 2, 2, 3, 1, 1, 2, 2, 2, 3};

            array.ToObservable()
                .DistinctUntilChanged()
                .Subscribe(x => Debug.Log(x));
        }
    }
}
