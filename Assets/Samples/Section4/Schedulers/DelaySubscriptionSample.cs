using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Schedulers
{
    public class DelaySubscriptionSample : MonoBehaviour
    {
        private void Start()
        {
            Observable.Return(1)
                .DoOnSubscribe(() =>
                {
                    Debug.Log("実際にSubscribeされた時間:" + Time.time);
                })
                .DelaySubscription(TimeSpan.FromSeconds(1))
                .Subscribe();

            Debug.Log("Subscribeを呼び出した時間:" + Time.time);
        }
    }
}