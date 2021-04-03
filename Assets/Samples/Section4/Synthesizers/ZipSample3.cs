using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    public class ZipSample3 : MonoBehaviour
    {
        private void Start()
        {
            var s1 = Observable.Return("A");
            var s2 = Observable.Return("B");
            var s3 = Observable.Return("C");

            // Observable.Zipを利用する場合
            Observable.Zip(s1, s2, s3).Subscribe(x => Debug.Log(x.Count));

            // IEnumerable<IObservable<T>>の拡張メソッドを利用する場合
            IEnumerable<IObservable<string>> streams = new[] { s1, s2, s3 };
            streams.Zip().Subscribe(x => Debug.Log(x.Count));
        }
    }
}