using System;
using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class IntervalSample : MonoBehaviour
    {
        private void Start()
        {
            //　Interval
            //　Subscribe()してから1秒間隔でメッセージを発行する
            // (1秒待ってから、1秒間隔でメッセージ発行）
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe()
                .AddTo(this); // GameObjectが破棄されたらDispose()

            // 比較:Observable.Timer
            // Subscribe()した直後にメッセージを発行し、それ以降1秒間隔で発行する
            Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
                .Subscribe()
                .AddTo(this); // GameObjectが破棄されたらDispose()
        }
    }
}