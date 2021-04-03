using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section5.ReactiveCommands
{
    public class ReactiveCommandSample3 : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;

        [SerializeField] private Button _button;

        void Start()
        {
            // uGUIのトグルに連動するReactiveCommand
            var reactiveCommand = _toggle
                .OnValueChangedAsObservable()
                .ToReactiveCommand<Unit>(false);

            // ButtonのOnClickイベントに応じてExecute()を自動実行する
            reactiveCommand.BindTo(_button);

            //  BindTo()はこれらと同じ
            /*
                _button.OnClickAsObservable().Subscribe(_ =>
                {
                    reactiveCommand.Execute(Unit.Default);
                });
                
                _toggle
                    .OnValueChangedAsObservable()
                    .SubscribeToInteractable(_button);
            */

            reactiveCommand.Subscribe(_ => { Debug.Log("Button clicked!"); });
        }
    }
}