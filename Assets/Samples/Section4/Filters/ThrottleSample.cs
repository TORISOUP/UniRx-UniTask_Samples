using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class ThrottleSample : MonoBehaviour
    {
        private void Start()
        {
            // GameObjectが1秒以上移動しなかったときに、その座標をログに出す
            transform
                .ObserveEveryValueChanged(x => x.position)
                .Throttle(TimeSpan.FromSeconds(1))
                .Subscribe(x => Debug.Log(x));
        }
    }
}