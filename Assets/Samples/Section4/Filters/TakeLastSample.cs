using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class TakeLastSample : MonoBehaviour
    {
        private void Start()
        {
            var array = new[] {1, 3, 4, 7, 2, 5, 9};

            array.ToObservable()
                // 最後から3つ取り出す
                .TakeLast(3)
                .Subscribe(
                    x => Debug.Log(x),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}