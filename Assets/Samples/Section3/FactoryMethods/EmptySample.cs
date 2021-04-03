using System;
using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class EmptySample : MonoBehaviour
    {
        private void Start()
        {
            Observable.Empty<int>()
                .Subscribe(
                    x => Debug.Log("OnNext:" + x),
                    ex => Debug.LogError("OnError:" + ex.Message),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}