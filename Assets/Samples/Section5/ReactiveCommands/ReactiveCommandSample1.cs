using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section5.ReactiveCommands
{
    public class ReactiveCommandSample1 : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;

        [SerializeField] private Button _button;

        private void Start()
        {
            // uGUIのトグルの状態を通知する
            IObservable<bool> toggleObservable = _toggle.OnValueChangedAsObservable();

            // uGUIのトグルに連動するReactiveCommand
            var reactiveCommand = new ReactiveCommand<Unit>(toggleObservable);

            // uGUIのボタンが押されたら、ReactiveCommandに実行命令を出す
            _button.OnClickAsObservable().Subscribe(_ =>
            {
                reactiveCommand.Execute(Unit.Default);
            });

            // ToggleがONの状態でButtonが押されたときのみメッセージが発行される
            // ToggleがOFFの状態の場合にButtonが押されても何もしない
            reactiveCommand.Subscribe(_ =>
            {
                Debug.Log("Button clicked!");
            });
        }
    }
}