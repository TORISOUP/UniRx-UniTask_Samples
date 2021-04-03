using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class SingleSample1 : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(1, 10)
                // 5のみを通過させる
                .Single(x => x == 5)
                .Subscribe(
                    x => Debug.Log(x),
                    ex => Debug.LogError(ex),
                    () => Debug.Log("OnCompleted"));
        }
    }
}