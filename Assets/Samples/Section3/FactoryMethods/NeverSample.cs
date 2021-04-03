using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class NeverSample : MonoBehaviour
    {
        private void Start()
        {
            Observable.Never<int>()
                .Subscribe(
                    x => Debug.Log("OnNext:" + x),
                    ex => Debug.LogError("OnError:" + ex.Message),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}