using System;
using UnityEngine;

namespace Samples.Section2.MyObservers
{
    class ObserveEventComponent : MonoBehaviour
    {
        [SerializeField] private CountDownEventProvider _countDownEventProvider;

        // Observerのインスタンス
        private PrintLogObserver<int> _printLogObserver;

        private IDisposable _disposable;

        private void Start()
        {
            // PrintLogObserverインスタンスを作成
            _printLogObserver = new PrintLogObserver<int>();

            // SubjectのSubscribeを呼び出して、observerを登録する
            _disposable = _countDownEventProvider
                .CountDownObservable.Subscribe(_printLogObserver);
        }

        private void OnDestroy()
        {
            // GameObject破棄時にイベント購読を中断する
            _disposable?.Dispose();
        }
    }
}