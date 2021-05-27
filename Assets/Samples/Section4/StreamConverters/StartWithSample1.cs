using UniRx;
using UnityEngine;

namespace Samples.Section4.StreamConverters
{
    public class StartWithSample1 : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(1, 3)
                // Subscribe()時に -1 を発行する
                .StartWith(-1)
                .Subscribe(x => Debug.Log(x));
        }
    }
}