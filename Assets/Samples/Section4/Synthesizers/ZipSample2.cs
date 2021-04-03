using UniRx;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    public class ZipSample2 : MonoBehaviour
    {
        private void Start()
        {
            var s1 = Observable.Return("A");
            var s2 = Observable.Return("B");
            var s3 = Observable.Return("C");
            var s4 = Observable.Return("D");
            var s5 = Observable.Return("E");
            var s6 = Observable.Return("F");
            var s7 = Observable.Return("G");

            s1.Zip(s2, s3, s4, s5, s6, s7,
                    (x1, x2, x3, x4, x5, x6, x7) => x1 + x2 + x3 + x4 + x5 + x6 + x7)
                .Subscribe(x => Debug.Log(x));
        }
    }
}