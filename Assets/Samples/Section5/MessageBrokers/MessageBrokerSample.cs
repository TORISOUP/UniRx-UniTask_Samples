using System;
using UniRx;
using UnityEngine;

namespace Samples.Section5.MessageBrokers
{
    public class MessageBrokerSample : MonoBehaviour
    {
        public void Start()
        {
            // MessageBroker.Default はアプリケーション全体で有効なインスタンス
            var messageBroker = UniRx.MessageBroker.Default;
            IMessagePublisher publisher = messageBroker;
            IMessageReceiver receiver = messageBroker;

            // 型を指定して購読ができる
            receiver.Receive<int>()
                .Subscribe(x => Debug.Log("int message : " + x)).AddTo(this);

            receiver.Receive<string>()
                .Subscribe(x => Debug.Log("string message : " + x)).AddTo(this);

            receiver.Receive<Exception>()
                .Subscribe(x => Debug.LogError("Exception : " + x)).AddTo(this);

            // 任意の型を発行できる
            publisher.Publish(123);
            publisher.Publish("Hello!");
            publisher.Publish(new Exception("Exception!"));
        }
    }
}