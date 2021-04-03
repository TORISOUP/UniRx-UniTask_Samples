using UniRx;
using UnityEngine;

namespace Samples.Section5.ObjectPools.UsePool
{
    /// <summary>
    /// BulletObjectPoolを提供する
    /// </summary>
    public class BulletObjectPoolProvider : MonoBehaviour
    {
        [SerializeField] private Bullet _prefab;

        private BulletObjectPool _objectPool;

        public BulletObjectPool Get()
        {
            // すでに準備済みならそちらを返す
            if (_objectPool != null) return _objectPool;

            // ObjectPoolを作成
            _objectPool = new BulletObjectPool(_prefab);

            // 事前にプールサイズを20に拡張しておく
            _objectPool.PreloadAsync(preloadCount: 20, threshold: 20).Subscribe();

            return _objectPool;
        }

        private void OnDestroy()
        {
            // プール内のBullet含めすべて破棄する
            _objectPool.Dispose();
        }
    }
}