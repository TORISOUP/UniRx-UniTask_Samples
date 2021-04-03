using UniRx;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    public class ZipSample1 : MonoBehaviour
    {
        private void Start()
        {
            var stream1 = new[] {"A", "B", "C", "D"}.ToObservable();
            var stream2 = Observable.Range(0, 3).Select(x => x);

            stream1
                .Zip(stream2, (x1, x2) => $"{x1}:{x2}")
                .Subscribe(x => Debug.Log(x));
        }
    }
}