using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class TakeSample : MonoBehaviour
    {
        private void Start()
        {
            var array = new[] {1, 3, 4, 7, 2, 5, 9};

            array.ToObservable()
                .Take(3) //先頭から3つ取り出す
                .Subscribe(
                    x => Debug.Log(x),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}