using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class SkipWhileSample : MonoBehaviour
    {
        private void Start()
        {
            var array = new[] {1, 3, 4, 7, 2, 5, 9};

            array.ToObservable()
                // 数値が5より小さい間は無視する
                .SkipWhile(x => x < 5)
                .Subscribe(x => Debug.Log(x));
        }
    }
}