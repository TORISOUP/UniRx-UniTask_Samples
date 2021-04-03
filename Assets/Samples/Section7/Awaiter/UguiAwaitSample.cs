using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section7.Awaiter
{
    /// <summary>
    /// uGUIの各種Eventのawait
    /// </summary>
    public class UguiAwaitSample : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private InputField _inputField;
        [SerializeField] private Slider _slider;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            WaitForButton(token).Forget();
            WaitForToggle(token).Forget();
            WaitForInputField(token).Forget();
            WaitForSlider(token).Forget();
        }

        private async UniTaskVoid WaitForButton(CancellationToken token)
        {
            // Button クリック
            using (var handler = _button.GetAsyncClickEventHandler(token))
            {
                while (!token.IsCancellationRequested)
                {
                    await handler.OnClickAsync();
                    Debug.Log("Button clicked");
                }
            }
        }

        private async UniTaskVoid WaitForToggle(CancellationToken token)
        {
            // Toggle 状態更新
            using (var handler = _toggle.GetAsyncValueChangedEventHandler(token))
            {
                while (!token.IsCancellationRequested)
                {
                    var isOn = await handler.OnValueChangedAsync();
                    Debug.Log($"Toggle state changed: {isOn}");
                }
            }
        }

        private async UniTaskVoid WaitForInputField(CancellationToken token)
        {
            // InputField テキスト入力完了
            using (var handler = _inputField.GetAsyncEndEditEventHandler(token))
            {
                while (!token.IsCancellationRequested)
                {
                    var text = await handler.OnEndEditAsync();
                    Debug.Log(text);
                }
            }
        }

        private async UniTaskVoid WaitForSlider(CancellationToken token)
        {
            // Slider 値更新
            using (var handler = _slider.GetAsyncValueChangedEventHandler(token))
            {
                while (!token.IsCancellationRequested)
                {
                    var sliderValue = await handler.OnValueChangedAsync();
                    Debug.Log($"Slider value changed: {sliderValue}");
                }
            }
        }
    }
}