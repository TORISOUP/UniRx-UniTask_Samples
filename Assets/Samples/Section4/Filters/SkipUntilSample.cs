using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class SkipUntilSample : MonoBehaviour
    {
        private void Start()
        {
            // カメラに描画されると発行されるイベント
            var onBecameVisible = this.OnBecameVisibleAsObservable();

            // カメラに描画されたタイミングから移動を開始する
            this.UpdateAsObservable()
                .SkipUntil(onBecameVisible)
                .Subscribe(_ =>
                {
                    transform.position += Vector3.forward * Time.deltaTime;
                });
        }
    }
}