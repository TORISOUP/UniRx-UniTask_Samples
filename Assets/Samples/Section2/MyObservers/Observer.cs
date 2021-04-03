using System;

namespace Samples.Section2.MyObservers
{
    /// <summary>
    /// 受信したメッセージをログに出力するObserver
    /// </summary>
    class PrintLogObserver<T> : IObserver<T>
    {
        public void OnCompleted()
        {
            UnityEngine.Debug.Log("OnCompleted!");
        }

        public void OnError(Exception error)
        {
            UnityEngine.Debug.LogError(error);
        }

        public void OnNext(T value)
        {
            UnityEngine.Debug.Log(value);
        }
    }


    /// <summary>
    /// シンプルなObserverインタフェース
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISimpleObserver<T>
    {
        /// <summary>
        /// イベント受信時に呼び出される関数
        /// </summary>
        /// <param name="message">受信したイベントメッセージ</param>
        void OnReceiveEvent(T message);
    }


    /// <summary>
    /// 受信したメッセージを保持し、後から参照できるObserver
    /// </summary>
    class CacheEventObserver<T> : IObserver<T>
    {
        public T ReceivedEvent { get; private set; }
        public bool IsCompleted { get; private set; }
        public Exception ReceivedError { get; private set; }

        public void OnCompleted()
        {
            IsCompleted = true;
        }

        public void OnError(Exception error)
        {
            ReceivedError = error;
        }

        public void OnNext(T value)
        {
            ReceivedEvent = value;
        }
    }

}