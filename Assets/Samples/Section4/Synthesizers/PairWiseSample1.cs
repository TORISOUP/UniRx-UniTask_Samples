using UniRx;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    public class PairWiseSample1 : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(0, 5)
                .Pairwise()
                .Subscribe(p =>
                {
                    // Current と Previous にそれぞれの値が入っている
                    Debug.Log($"{p.Previous}:{p.Current}");
                });
        }
    }
}