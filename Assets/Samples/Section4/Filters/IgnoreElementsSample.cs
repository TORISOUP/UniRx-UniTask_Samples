using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class IgnoreElementsSample : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(0, 10)
                .IgnoreElements()
                .Subscribe(
                    x => Debug.Log(x),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}