using UniRx;
using UnityEngine;

namespace Samples.Section3.ReactiveProperty
{
    public class ReactivePropertySample : MonoBehaviour
    {
        private void Start()
        {
            //int型を扱うReactiveProperty
            //初期値は100
            var health = new ReactiveProperty<int>(100);

            //Valueにアクセスすれば現在設定されている値を読み取れる
            Debug.Log("現在の値:" + health.Value);

            //ReactivePropertyを直接Subscribeできる
            //Subscribeしたタイミングで現在の値が自動的に発行される
            health.Subscribe(
                x => Debug.Log("通知された値:" + x),
                () => Debug.Log("OnCompleted"));

            //Valueに値を設定すると、同時にOnNextが発行される
            health.Value = 50;

            Debug.Log("現在の値:" + health.Value);

            // Dispose()するとOnCompletedメッセージが発行される
            health.Dispose();
        }
    }
}