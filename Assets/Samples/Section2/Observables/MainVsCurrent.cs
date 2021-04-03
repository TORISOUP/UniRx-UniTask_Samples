using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class MainVsCurrent : MonoBehaviour
    {
        void Start()
        {
            // Unityのコルーチンを用いて3秒計測
            Observable
                .Timer(TimeSpan.FromSeconds(3), Scheduler.MainThread)
                .Subscribe();

            // メインスレッドをThread.Sleepして3秒計測
            Observable
                .Timer(TimeSpan.FromSeconds(3), Scheduler.CurrentThread)
                .Subscribe();
        }
    }
}