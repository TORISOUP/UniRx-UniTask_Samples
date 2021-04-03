using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class FirstSample2 : MonoBehaviour
    {
        private void Start()
        {
            Observable.Empty<int>() //OnCompletedのみを発行する
                .First()
                .Subscribe(
                    x => Debug.Log(x),
                    ex => Debug.LogError(ex),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}