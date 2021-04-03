using System;
using UniRx;
using UnityEngine;

namespace Samples.Section5.ObjectPools.UsePool
{
    public class Enemy : MonoBehaviour
    {
        /// <summary>
        /// BulletObjectPoolProviderはインスペクタ経由で設定
        /// </summary>
        [SerializeField] 
        private BulletObjectPoolProvider _bulletObjectPoolProvider;

        private BulletObjectPool _objectPool;

        private void Start()
        {
            // ObjectPoolを取得
            _objectPool = _bulletObjectPoolProvider.Get();

            // 定期的に弾を発射する
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ => ShootBullets()).AddTo(this);
        }

        private void ShootBullets()
        {
            // 3way
            for (var i = -1; i < 2; i++)
            {
                var b = _objectPool.Rent(); // Bulletインスタンスを取得する

                // 弾の進む方向
                var dir = Quaternion.AngleAxis(i * 30, transform.up) * transform.forward;

                // 弾の配置
                var initPos = transform.position + dir * 1.0f;

                // 弾の初期位置と速度を設定
                b.Initialize(initPos, dir * 3.0f);

                // 弾を発射し、使い終わったらObjectPoolに戻す
                b.OnFinishedAsync
                    .Take(1)
                    .Subscribe(_ =>
                    {
                        _objectPool.Return(b);
                    });
            }
        }
    }
}