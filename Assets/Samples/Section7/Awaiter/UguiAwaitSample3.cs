using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section7.Awaiter
{
    /// <summary>
    /// uGUIの各種Eventのawait
    /// </summary>
    public class UguiAwaitSample3 : MonoBehaviour
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
            // Buttonクリック
            await _button
                .OnClickAsAsyncEnumerable(token)
                .ForEachAsync(_ => Debug.Log("Button clicked"), token);
        }

        private async UniTaskVoid WaitForToggle(CancellationToken token)
        {
            // Toggle Onのときのみ表示
            await _toggle.OnValueChangedAsAsyncEnumerable(token)
                .Where(x => x)
                .ForEachAsync(x => Debug.Log($"Toggle is On"), token);
        }

        private async UniTaskVoid WaitForInputField(CancellationToken token)
        {
            // InputField 5文字以上入力されたときに表示
            await _inputField.OnValueChangedAsAsyncEnumerable(token)
                .Where(x => x.Length >= 5)
                .ForEachAsync(x => Debug.Log(x), token);
        }

        private async UniTaskVoid WaitForSlider(CancellationToken token)
        {
            // Slider 0.5以上の値のときのみ表示
            _slider.OnValueChangedAsAsyncEnumerable()
                .Where(x => x > 0.5f)
                .ForEachAsync(x => Debug.Log($"Slider value changed: {x}"), token);
        }
    }
}