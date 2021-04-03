using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class SingleSample2 : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(1, 10)
                // 2の約数を1つだけ通す
                // 2回目が到達した場合はOnErrorメッセージ
                .Single(x => x % 2 == 0)
                .Subscribe(
                    x => Debug.Log(x),
                    ex => Debug.LogError(ex),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}