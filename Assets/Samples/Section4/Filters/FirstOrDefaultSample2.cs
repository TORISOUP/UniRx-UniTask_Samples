using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class FirstOrDefaultSample2 : MonoBehaviour
    {
        private void Start()
        {
            // OnCompletedのみを発行する
            Observable.Empty<int>()
                .FirstOrDefault()
                .Subscribe(
                    x => Debug.Log(x),
                    ex => Debug.LogError(ex),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}