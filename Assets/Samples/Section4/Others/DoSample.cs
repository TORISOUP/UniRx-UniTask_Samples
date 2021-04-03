using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Others
{
    public class DoSample : MonoBehaviour
    {
        private void Start()
        {
            // 10秒数える
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Do(x => Debug.Log(x)) //タイマの途中経過をログに出す
                .Take(10)
                .Subscribe(
                    _ => { },
                    () => Debug.Log("10秒経ちました")
                );
        }
    }
}