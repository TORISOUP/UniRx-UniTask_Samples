using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    class ContinueWithTest : MonoBehaviour
    {
        void Start()
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(3)
                .ContinueWith(ObservableWWW.Get("https://unity3d.com/jp/"))
                .Subscribe(x => Debug.Log(x));
        }
    }
}