using System;
using UniRx;
using UnityEngine;

namespace Samples.Section5.ObjectPools.NoPool
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;

        private void Start()
        {
            // 定期的に弾を発射する
            Observable
                .Interval(TimeSpan.FromSeconds(1.0f))
                .Subscribe(_ => ShootBullets())
                .AddTo(this);
        }

        // 球を発射する処理
        private void ShootBullets()
        {
            // 3way
            for (var i = -1; i < 2; i++)
            {
                // Bulletを都度生成
                var b = Instantiate<Bullet>(_bulletPrefab);

                // Bulletの進む方向
                var dir = 
                    Quaternion.AngleAxis(i * 30, transform.up) * transform.forward;

                // Bulletの座標を調整して速度を設定
                b.transform.position += dir * 1.0f;
                b.AddVelocity(dir * 3.0f);
            }
        }
    }
}