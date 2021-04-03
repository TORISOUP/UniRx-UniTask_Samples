using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    public class MergeSample2 : MonoBehaviour
    {
        private void Start()
        {
            // Observableを扱うObservable
            IObservable<IObservable<int>> streams =
                Observable.Range(1, 3, Scheduler.Immediate)
                    .Select(x =>
                    {
                        return Observable.Range(x * 100, 3, Scheduler.Immediate);
                    });

            streams
                .Merge() // 1つのObservableにまとめる
                .Subscribe(x => Debug.Log(x));
        }
    }
}