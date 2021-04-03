using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section7.Awaiter
{
    /// <summary>
    /// uGUIの各種Eventのawait
    /// </summary>
    public class UguiAwaitSample2 : MonoBehaviour
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
            await _button.OnClickAsync(token);
            Debug.Log("Button clicked");
        }

        private async UniTaskVoid WaitForToggle(CancellationToken token)
        {
            // Toggle 状態更新
            var isOn = await _toggle.OnValueChangedAsync(token);
            Debug.Log($"Toggle state changed: {isOn}");
        }

        private async UniTaskVoid WaitForInputField(CancellationToken token)
        {
            // InputField テキスト入力完了
            var text = await _inputField.OnValueChangedAsync(token);
            Debug.Log(text);
        }

        private async UniTaskVoid WaitForSlider(CancellationToken token)
        {
            // Slider 値更新
            var sliderValue = await _slider.OnValueChangedAsync(token);
            Debug.Log($"Slider value changed: {sliderValue}");
        }
    }
}