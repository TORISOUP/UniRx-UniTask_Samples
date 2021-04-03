using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Samples.Section5.ObjectPools.UsePool
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private readonly Subject<Unit> _finishedSubject = new Subject<Unit>();

        /// <summary>
        /// オブジェクトを使い終わったことを通知する
        /// </summary>
        public IObservable<Unit> OnFinishedAsync => _finishedSubject.Take(1);

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            // なにかにぶつかったときの処理
            this.OnTriggerEnterAsObservable()
                .Subscribe(_ => OnHit());
        }

        /// <summary>
        /// Bulletの初期設定を行う
        /// </summary>
        public void Initialize(Vector3 initPosition, Vector3 velocity)
        {
            transform.position = initPosition;

            Observable.NextFrame(FrameCountType.FixedUpdate)
                .TakeUntilDisable(this)
                .Subscribe(_ => _rigidbody.AddForce(velocity, ForceMode.VelocityChange))
                .AddTo(this);

            // 3秒たったら終了する
            Observable.Timer(TimeSpan.FromSeconds(3))
                .TakeUntilDisable(this)
                .TakeUntilDestroy(this)
                .Subscribe(_ => Finish());
        }

        private void OnHit()
        {
            Debug.Log("ぶつかった！");

            Finish();
        }

        /// <summary>
        /// インスタンスを使い終わったときに実行する
        /// </summary>
        private void Finish()
        {
            // 速度をゼロにする
            _rigidbody.velocity = Vector3.zero;

            // 使い終わったイベント発行
            _finishedSubject.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            _finishedSubject.Dispose();
        }
    }
}