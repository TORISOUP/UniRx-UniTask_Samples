using System;
using System.Collections.Generic;
using UniRx;

namespace Samples.Section2.MySubjects
{
    /// <summary>
    /// 独自実装Subject
    /// </summary>
    public class MySubject<T> : ISubject<T>, IDisposable
    {
        public bool IsStopped { get; } = false;
        public bool IsDisposed { get; } = false;

        private readonly object _lockObject = new object();

        /// <summary>
        /// 途中で発生した例外
        /// </summary>
        private Exception error;

        /// <summary>
        /// 自身を購読しているObserverリスト
        /// </summary>
        private List<IObserver<T>> observers;

        public MySubject()
        {
            observers = new List<IObserver<T>>();
        }

        /// <summary>
        /// IObserver.OnNextの実装
        /// </summary>
        public void OnNext(T value)
        {
            if (IsStopped) return;
            lock (_lockObject)
            {
                ThrowIfDisposed();

                //自身を行動しているObserver全員へメッセージをばらまく
                foreach (var observer in observers)
                {
                    observer.OnNext(value);
                }
            }
        }

        /// <summary>
        /// IObserver.OnErrorの実装
        /// </summary>
        public void OnError(Exception error)
        {
            lock (_lockObject)
            {
                ThrowIfDisposed();
                if (IsStopped) return;
                this.error = error;

                try
                {
                    foreach (var observer in observers)
                    {
                        observer.OnError(error);
                    }
                }
                finally
                {
                    Dispose();
                }
            }
        }

        /// <summary>
        /// IObserver.OnCompletedの実装
        /// </summary>
        public void OnCompleted()
        {
            lock (_lockObject)
            {
                ThrowIfDisposed();
                if (IsStopped) return;
                try
                {
                    foreach (var observer in observers)
                    {
                        observer.OnCompleted();
                    }
                }
                finally
                {
                    Dispose();
                }
            }
        }

        /// <summary>
        /// IObservable.Subscribeの実装
        /// </summary>
        public IDisposable Subscribe(IObserver<T> observer)
        {
            lock (_lockObject)
            {
                if (IsStopped)
                {
                    // 既に動作を終了しているならOnErrorまたはOnCompletedを発行する
                    if (error != null)
                    {
                        observer.OnError(error);
                    }
                    else
                    {
                        observer.OnCompleted();
                    }

                    return Disposable.Empty;
                }

                observers.Add(observer); //リストに追加
                return new Subscription(this, observer);
            }
        }

        private void ThrowIfDisposed()
        {
            if (IsDisposed) throw new ObjectDisposedException("MySubject");
        }

        /// <summary>
        /// SubscribeのDisposeを実現するために用いるinner class
        /// </summary>
        private sealed class Subscription : IDisposable
        {
            private readonly IObserver<T> _observer;
            private readonly MySubject<T> _parent;

            public Subscription(MySubject<T> parent, IObserver<T> observer)
            {
                this._parent = parent;
                this._observer = observer;
            }

            public void Dispose()
            {
                // DisposeされたらObserverリストから消去する
                _parent.observers.Remove(_observer);
            }
        }

        public void Dispose()
        {
            lock (_lockObject)
            {
                if (!IsDisposed)
                {
                    observers.Clear();
                    observers = null;
                    error = null;
                }
            }
        }
    }
}