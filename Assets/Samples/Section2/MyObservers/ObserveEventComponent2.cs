using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.MyObservers
{
    class ObserveEventComponent2 : MonoBehaviour
    {
        [SerializeField] private CountDownEventProvider _countDownEventProvider;

        // Observerのインスタンス
        private PrintLogObserver<int> _printLogObserver;

        private IDisposable _disposable;

        private void Start()
        {
            // SubjectのSubscribeを呼び出して、observerを登録する
            _disposable = _countDownEventProvider
                .CountDownObservable
                .Subscribe(
                    x => Debug.Log(x), //OnNext
                    ex => Debug.LogError(ex), //OnError
                    () => Debug.Log("OnCompleted!")); //OnCompleted
        }

        private void OnDestroy()
        {
            // GameObject破棄時にイベント購読を中断する
            _disposable?.Dispose();
        }
    }
}