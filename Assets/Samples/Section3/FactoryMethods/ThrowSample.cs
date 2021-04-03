using System;
using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class ThrowSample : MonoBehaviour
    {
        private void Start()
        {
            // intを扱うObservableからOnErrorメッセージを発行する
            Observable.Throw<int>(new Exception("エラーが発生しました"))
                .Subscribe(
                    x => Debug.Log("OnNext:" + x),
                    ex => Debug.LogError("OnError:" + ex.Message),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}