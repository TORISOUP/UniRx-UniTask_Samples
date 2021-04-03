using System;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section5.AsyncObjectPools
{
    public class EnemySpawner : MonoBehaviour, IPrefabProvidable<Enemy>
    {
        private EnemyAsyncObjectPool _pool;
        [SerializeField] private Enemy _prefab;

        private void Start()
        {
            _pool = new EnemyAsyncObjectPool(this);

            // 定期的に弾を発射する
            Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ => ShootBullets()).AddTo(this);
            _pool.RentAsync().Subscribe();
        }

        private void ShootBullets()
        {
            _pool.RentAsync().Subscribe(x =>
            {
                x.transform.position = Vector3.zero;
                x.GetComponent<Rigidbody>().velocity = Vector3.forward;
                
                Observable.Timer(TimeSpan.FromSeconds(3))
                    .Subscribe(_ => _pool.Return(x));
            });
        }

        public async UniTask<Enemy> LoadPrefabAsync()
        {
            await UniTask.Delay(100);
            return Instantiate(_prefab);
        }
    }
}