using UniRx;
using UnityEngine;

namespace Samples.Section4.converters
{
    public class CastSample : MonoBehaviour
    {
        private void Start()
        {
            var objects = new object[]
            {
                "hoge",
                "fuga",
                'a',
                -1,
                "fuga",
                'Z',
                0.1
            };

            objects.ToObservable()
                .Cast<object, string>()
                .Subscribe(
                    x => Debug.Log(x),
                    ex => Debug.LogError(ex),
                    () => Debug.Log("OnCompleted"));
        }
    }
}