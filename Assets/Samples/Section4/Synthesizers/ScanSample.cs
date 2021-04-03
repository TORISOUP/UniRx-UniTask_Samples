using UniRx;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    internal class ScanSample : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(1, 5)
                .Scan(0, (pre, cur) => pre + cur)
                .Subscribe(x => Debug.Log(x));
        }
    }
}