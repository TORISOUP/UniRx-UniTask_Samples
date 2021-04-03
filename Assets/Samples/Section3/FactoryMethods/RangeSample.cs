using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class RangeSample : MonoBehaviour
    {
        private void Start()
        {
            //0から５個、連続した整数値を発行する
            Observable.Range(start: 0, count: 5)
                .Subscribe(
                    x => Debug.Log("OnNext:" + x),
                    ex => Debug.LogError("OnError:" + ex.Message),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}