using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section5.ModelViewReactivePresenter
{
    /// <summary>
    /// Presenter
    /// </summary>
    public class CountPresenter : MonoBehaviour
    {
        // Modelへの参照
        [SerializeField] private CountModel _countModel;

        // View(uGUIコンポーネント)への参照
        [SerializeField] private InputField _inputField;

        [SerializeField] private Button _upButton;
        [SerializeField] private Button _downButton;
        [SerializeField] private Slider _slider;

        private void Start()
        {
            // UniRxの機能を活用して、ModelとViewを相互接続する

            // Model -> View
            _countModel.Current
                .Subscribe(x =>
                {
                    // Modelの値が変動したら
                    // InputFieldとSliderの状態を更新
                    _inputField.text = x.ToString();
                    _slider.value = x;
                })
                .AddTo(this);

            // -----

            // View -> Model

            // InputFieldが更新されたらModelに反映
            _inputField
                .OnValueChangedAsObservable()
                .Select(x =>
                {
                    var isSucceed = int.TryParse(x, out var value);
                    return (isSucceed, value);
                })
                .Where(x => x.isSucceed)
                .Subscribe(x => _countModel.UpdateCount(x.value))
                .AddTo(this);

            // Sliderが更新されたらModelに反映
            _slider
                .OnValueChangedAsObservable()
                .Subscribe(x => _countModel.UpdateCount((int) x))
                .AddTo(this);

            // Buttonが押されたらModelに反映
            Observable.Merge(
                    _upButton.OnClickAsObservable().Select(_ => +1),
                    _downButton.OnClickAsObservable().Select(_ => -1)
                ).Subscribe(value =>
                {
                    _countModel.UpdateCount(_countModel.Current.Value + value);
                })
                .AddTo(this);
        }
    }
}