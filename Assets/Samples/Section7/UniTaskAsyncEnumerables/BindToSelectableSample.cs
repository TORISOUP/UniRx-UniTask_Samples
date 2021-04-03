using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section7.UniTaskAsyncEnumerables
{
    public class BindToSelectableSample : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Button _button;

        private void Start()
        {
            // Toggleにチェックが入っているときのみ
            // Buttonを押せる状態にする
            _toggle
                .OnValueChangedAsAsyncEnumerable()
                .BindTo(_button, this.GetCancellationTokenOnDestroy());

        }
    }
}
