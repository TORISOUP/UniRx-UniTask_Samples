using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class RepeatSample : MonoBehaviour
    {
        private void Start()
        {
            //"yes"を３個発行する
            Observable.Repeat(value: "yes", repeatCount: 3)
                .Subscribe(
                    x => Debug.Log("OnNext:" + x),
                    ex => Debug.LogError("OnError:" + ex.Message),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}