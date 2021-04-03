using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class UnityEventToUniTaskSample : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private InputField _inputField;
    [SerializeField] private Slider _slider;

    private async UniTaskVoid Start()
    {
        // Button クリック
        UniTask onClickButton = _button.OnClickAsync();

        // Toggle 状態更新
        UniTask<bool> toggleEventHandler = _toggle.OnValueChangedAsync();

        // InputField テキスト入力完了
        UniTask<string> inputFieldEventHandler = _inputField.OnEndEditAsync();

        // Slider 値更新
        UniTask<float> sliderEventHandler = _slider.OnValueChangedAsync();
    }
}