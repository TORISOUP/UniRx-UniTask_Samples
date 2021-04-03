using UniRx;
using UnityEngine;

namespace Samples.Section3.ReactiveProperty
{
    public class ReactivePropertySample3 : MonoBehaviour
    {
        private void Start()
        {
            var health = new ReactiveProperty<int>(100);

            health
                // Subscribe()直後のOnNextメッセージを無視する
                .SkipLatestValueOnSubscribe()
                .Subscribe(x => Debug.Log("通知された値:" + x));

            health.Dispose();
        }
    }
}