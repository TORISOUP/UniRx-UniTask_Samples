using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Samples.Section4.StreamConverters
{
    class TimeIntervalSample : MonoBehaviour
    {
        void Start()
        {
            var rigidBody = GetComponent<Rigidbody>();

            // ジャンプボタンが入力されたら通知されるObservable
            IObservable<Unit> jumpButton =
                this.UpdateAsObservable().Where(_ => Input.GetButtonDown("Jump"));

            // ジャンプボタンが押されたら次のFixedUpdateでジャンプ処理をする
            jumpButton.BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ => { rigidBody.AddForce(Vector3.up, ForceMode.VelocityChange); });
        }
    }
}