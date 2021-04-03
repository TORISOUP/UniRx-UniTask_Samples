using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class MessageSample : MonoBehaviour
    {
        /// <summary>
        /// 残り時間
        /// </summary>
        [SerializeField] private float _countTimeSeconds = 30f;
        
        /// <summary>
        /// 時間切れを通知するObservable
        /// </summary>
        public IObservable<Unit> OnTimeUpAsyncSubject => _onTimeUpAsyncSubject;

        /// <summary>
        /// AsyncSubject(メッセージを1度だけ発行できるSubject)
        /// </summary>
        private readonly AsyncSubject<Unit> _onTimeUpAsyncSubject
            = new AsyncSubject<Unit>();
        
        private IDisposable _disposable;

        private void Start()
        {
            // 指定時間経過したらメッセージを通知する
            _disposable = Observable
                .Timer(TimeSpan.FromSeconds(_countTimeSeconds))
                .Subscribe(_ =>
                {
                    // Timerが発火したら、
                    // Unit型のメッセージを発行する
                    _onTimeUpAsyncSubject.OnNext(Unit.Default);
                    _onTimeUpAsyncSubject.OnCompleted();
                });
        }

        private void OnDestroy()
        {
            // Observableがまだ動いていたら止める
            _disposable?.Dispose();

            // AsyncSubjectを破棄
            _onTimeUpAsyncSubject.Dispose();
        }
    }
}