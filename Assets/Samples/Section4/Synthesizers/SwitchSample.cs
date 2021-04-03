using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    public class SwitchSample : MonoBehaviour
    {
        private Transform _target;

        private void Start()
        {
            // IObservable<Vector3> を発行するObservable
            // 衝突したオブジェクトから現在座標を発行するObservableを生成するObservable
            IObservable<IObservable<Vector3>> targetObservable =
                this.OnCollisionEnterAsObservable()
                    .Select(x =>
                    {
                        // 最後に触れたオブジェクトの現在座標を発行するObservable
                        var target = x.gameObject;
                        return CreatePositionObservable(target);
                    });

            // 最後に衝突したオブジェクトを追尾する
            targetObservable
                .Switch() // 最後に衝突したオブジェクトに購読対象を切り替え
                .Subscribe(target =>
                {
                    // ゆっくり追従
                    transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime);
                });
        }

        /// <summary>
        /// 対象のGameObjectの座標を毎フレーム発行するObservableを生成する
        /// </summary>
        private IObservable<Vector3> CreatePositionObservable(GameObject target)
        {
            return target
                .UpdateAsObservable()
                .Select(_ => target.transform.position);
        }
    }
}