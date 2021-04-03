using System.Collections;
using UniRx;
using UnityEngine;

namespace Samples.Section3.ReactiveProperty
{
    /// <summary>
    /// カウントダウンするタイマ
    /// </summary>
    class ReactivePropertyTimerSample : MonoBehaviour
    {
        //実体としてReactivePropertyを定義
        [SerializeField] 
        private IntReactiveProperty _current = new IntReactiveProperty(60);

        /// <summary>
        /// 現在のタイマの値（読み取り専用）
        /// ReactiveProperty を IReadOnlyReactiveProperty にアップキャスト
        /// </summary>
        public IReadOnlyReactiveProperty<int> CurrentTime => _current;


        private void Start()
        {
            StartCoroutine(CountDownCoroutine());
            _current.AddTo(this);
        }

        private IEnumerator CountDownCoroutine()
        {
            while (_current.Value > 0)
            {
                // 1秒に1つずつ値を更新する
                _current.Value--;
                yield return new WaitForSeconds(1);
            }
        }
    }
}