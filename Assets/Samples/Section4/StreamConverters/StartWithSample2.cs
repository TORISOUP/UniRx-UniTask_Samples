using UniRx;
using UnityEngine;

namespace Samples.Section4.StreamConverters
{
    public class StartWithSample2 : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(1, 3)
                // 最初の値が乱数となる
                .StartWith(() => Random.Range(-10, -1))
                .Subscribe(x => Debug.Log(x));
        }
    }
}