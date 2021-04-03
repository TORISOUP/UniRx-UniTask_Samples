using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    public class MergeSample3 : MonoBehaviour
    {
        private void Start()
        {
            IEnumerable<IObservable<int>> streams = new[]
            {
                Observable.Range(100, 3),
                Observable.Range(200, 3),
                Observable.Range(300, 3)
            };

            streams
                .Merge() // 配列を1つのObservableにまとめる
                .Subscribe(x => Debug.Log(x));
        }
    }
}