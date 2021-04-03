using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class TakeUntilDestroySample : MonoBehaviour
    {
        private void Start()
        {
            // このGameObjectがDestroyされるまでカウントアップし続ける
            Observable.Interval(TimeSpan.FromSeconds(1))
                .TakeUntilDestroy(gameObject)
                .Subscribe(x => Debug.Log(x));
        }
    }
}