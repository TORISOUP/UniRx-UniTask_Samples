using UniRx;
using UnityEngine;

namespace Samples.Section5.ModelViewReactivePresenter
{
    /// <summary>
    /// Model
    /// </summary>
    public class CountModel : MonoBehaviour
    {
        /// <summary>
        /// 整数値を扱うReactiveProperty
        /// </summary>
        [SerializeField] 
        private IntReactiveProperty _current
            = new IntReactiveProperty(0);

        /// <summary>
        /// Presenter向けに公開するプロパティ
        /// </summary>
        public IReadOnlyReactiveProperty<int> Current => _current;

        /// <summary>
        /// 整数値の更新を行う
        /// </summary>
        public void UpdateCount(int value)
        {
            // 0-100の範囲に収めてから設定
            _current.Value = Mathf.Clamp(value, 0, 100);
        }
    }
}