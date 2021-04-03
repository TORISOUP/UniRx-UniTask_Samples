using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class ReturnSample : MonoBehaviour
    {
        private void Start()
        {
            Observable.Return(100)
                .Subscribe(
                    x => Debug.Log("OnNext:" + x),
                    ex => Debug.LogError("OnError:" + ex.Message),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}