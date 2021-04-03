using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class OfTypeSample : MonoBehaviour
    {
        private void Start()
        {
            object[] objects = {1, "hoge", 1.0f, "fuga", 'Z', 0.1};

            objects.ToObservable()
                //string型にキャストできるもののみを通す
                .OfType<object, string>()
                .Subscribe(x => Debug.Log(x));
        }
    }
}