using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Samples.Section5.ObjectPools.NoPool
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            // なにかに衝突したら消える
            this.OnTriggerEnterAsObservable()
                .Subscribe(_ => Destroy(gameObject));

            // 3秒たったら消える
            Destroy(gameObject, 3.0f);
        }

        /// <summary>
        /// 速度を与える
        /// </summary>
        public void AddVelocity(Vector3 velocity)
        {
            Observable.NextFrame(FrameCountType.FixedUpdate)
                .Subscribe(_ => _rigidbody.AddForce(velocity, ForceMode.VelocityChange))
                .AddTo(this);
        }
    }
}