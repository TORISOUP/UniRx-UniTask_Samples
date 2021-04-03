using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section5.ReactiveCommands
{
    public class ReactiveCommandSample4 : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;

        [SerializeField] private Button _button;

        void Start()
        {
            // uGUIのトグルに連動するReactiveCommand
            var reactiveCommand = _toggle
                .OnValueChangedAsObservable()
                .ToReactiveCommand<Unit>(false);

            // BindTo()とSubscribeを同時に実行できる
            reactiveCommand.BindToOnClick(_button, _ =>
            {
                // Toggleがtrueのときに、Buttonが押されると実行
                Debug.Log("Button clicked!");
            });
        }
    }
}