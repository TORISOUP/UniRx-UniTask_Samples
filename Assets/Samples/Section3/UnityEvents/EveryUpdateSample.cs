using UniRx;
using UnityEngine;

namespace Samples.Section3.UnityEvents
{
    public class EveryUpdateSample : MonoBehaviour
    {
        private void Start()
        {
            // 毎フレームオブジェクトの座標を更新する
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    transform.position += Vector3.forward * Time.deltaTime;
                })
                .AddTo(this); // GameObjectが破棄されたら確実に止める
        }
    }
}