using System;
using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class TimerSample : MonoBehaviour
    {
        private void Start()
        {
            // 1秒後に発火するObservable
            var timer = Observable.Timer(dueTime: TimeSpan.FromSeconds(1));

            Debug.Log("Subscribeした時刻:" + Time.time);

            timer.Subscribe(x =>
            {
                Debug.Log("OnNextが発行された時刻:" + Time.time);
                Debug.Log("OnNextの中身:" + x);
            }, () => Debug.Log("OnCompleted"));
        }
    }
}