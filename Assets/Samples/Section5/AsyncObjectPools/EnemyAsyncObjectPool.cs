using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Toolkit;

namespace Samples.Section5.AsyncObjectPools
{
    public class EnemyAsyncObjectPool : AsyncObjectPool<Enemy>
    {
        /// <summary>
        /// BulletのPrefabを非同期に提供する
        /// </summary>
        private readonly IPrefabProvidable<Enemy> _enemyPrefabProvider;

        public EnemyAsyncObjectPool(IPrefabProvidable<Enemy> prefabProvider)
        {
            _enemyPrefabProvider = prefabProvider;
        }

        protected override IObservable<Enemy> CreateInstanceAsync()
        {
            // Prefabの非同期ロードが終わってからInstantiateする
            return _enemyPrefabProvider.LoadPrefabAsync().ToObservable();
        }
    }
}