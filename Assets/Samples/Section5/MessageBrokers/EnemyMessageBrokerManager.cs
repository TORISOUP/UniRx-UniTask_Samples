using System;
using UniRx;

namespace Samples.Section5.MessageBrokers
{
    /// <summary>
    /// 型によって成約をつけたMessageBroker
    /// </summary>
    public abstract class MessageBroker<TBase> : MessageBroker
    {
        public new void Publish<T>(T message) where T : TBase
        {
            base.Publish(message);
        }

        public new IObservable<T> Receive<T>() where T : TBase
        {
            return base.Receive<T>();
        }
    }

    /// <summary>
    /// EnemyEventを扱うMessageBroker
    /// </summary>
    public class EnemyEventMessageBroker : MessageBroker<IEnemyEvent>
    {
    }

    /// <summary>
    /// EnemyEventを扱うMessageBroker
    /// </summary>
    public class PlayerEventMessageBroker : MessageBroker<IPlayerEvent>
    {
    }
}