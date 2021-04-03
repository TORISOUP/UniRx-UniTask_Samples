using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Samples.Section3.FactoryMethods
{
    public class FromEventSample : MonoBehaviour
    {
        /// <summary>
        /// オリジナルのEventArgs
        /// </summary>
        public sealed class MyEventArgs : EventArgs
        {
            public int MyProperty { get; set; }
        }

        /// <summary>
        /// MyEventArgsを扱うイベントハンドラ
        /// </summary>
        event EventHandler<MyEventArgs> _onEvent;

        /// <summary>
        /// int型を引数にとるAction
        /// </summary>
        event Action<int> _callBackAction;

        /// <summary>
        /// uGUIのボタン
        /// </summary>
        [SerializeField] private Button _uiButton;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        private void Start()
        {
            // FromEventPatternを使う場合
            // (sender, eventArgs)を両方使ってイベントをIObservable<MyEventArgs>に変換する
            Observable.FromEventPattern<EventHandler<MyEventArgs>, MyEventArgs>(
                    h => h.Invoke, h => _onEvent += h, h => _onEvent -= h)
                .Subscribe()
                .AddTo(_disposables);

            // FromEventを使う場合
            // eventArgsのみを使ってイベントをIObservable<MyEventArgs>に変換する
            Observable.FromEvent<EventHandler<MyEventArgs>, MyEventArgs>(
                    h => (sender, e) => h(e), h => _onEvent += h, h => _onEvent -= h)
                .Subscribe()
                .AddTo(_disposables);

            // Action<T>を使ったイベントもObservable<T>にも変換できる
            Observable.FromEvent<int>(
                    h => _callBackAction += h, h => _callBackAction -= h)
                .Subscribe()
                .AddTo(_disposables);

            // UnityEventからObservableにも変換可能
            Observable.FromEvent<UnityAction>(
                    h => new UnityAction(h),
                    h => _uiButton.onClick.AddListener(h),
                    h => _uiButton.onClick.RemoveListener(h)
                ).Subscribe()
                .AddTo(_disposables);

            // ただし、UnityEventからObservableに変換する場合は
            // FromEventの糖衣構文として「AsObservable()」が用意されている
            _uiButton.onClick.AsObservable().Subscribe().AddTo(_disposables);
        }

        private void OnDestroy()
        {
            //破棄された時にまとめてDisposeする
            _disposables.Dispose();
        }
    }
}