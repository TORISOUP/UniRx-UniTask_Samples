using UniRx;
using UnityEngine;

namespace Samples.Section4.converters
{
    public class SelectSample : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(1, 5)
                .Select(x => x * 10) // 10倍する
                .Subscribe(x => Debug.Log(x));
        }
    }
}