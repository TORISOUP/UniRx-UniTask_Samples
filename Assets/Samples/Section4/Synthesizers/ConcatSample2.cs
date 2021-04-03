using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    public class ConcatSample2 : MonoBehaviour
    {
        private void Start()
        {
            // Observableを扱うObservable
            IObservable<IObservable<int>> streams =
                Observable
                    .Range(1, 3)
                    .Select(x =>
                    {
                        return Observable.Range(x * 100, 3);
                    });

            streams
                .Concat()
                .Subscribe(x => Debug.Log(x));
        }
    }
}