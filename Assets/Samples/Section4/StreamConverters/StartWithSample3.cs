using UniRx;
using UnityEngine;

namespace Samples.Section4.StreamConverters
{
    public class StartWithSample3 : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(1, 3)
                // 複数要素を指定することもできる
                .StartWith(-3, -2, -1)
                .Subscribe(x => Debug.Log(x));
        }
    }
}