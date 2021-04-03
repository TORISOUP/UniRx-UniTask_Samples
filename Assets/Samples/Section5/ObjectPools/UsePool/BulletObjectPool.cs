using UniRx.Toolkit;
using UnityEngine;

namespace Samples.Section5.ObjectPools.UsePool
{
    /// <summary>
    /// Bulletを管理するObjectPool
    /// </summary>
    public class BulletObjectPool : ObjectPool<Bullet>
    {
        /// <summary>
        /// BulletのPrefab
        /// </summary>
        private readonly Bullet _prefab;

        /// <summary>
        /// ヒエラルキー上で親となるTransform
        /// </summary>
        private readonly Transform _root;

        public BulletObjectPool(Bullet prefab)
        {
            _prefab = prefab;

            // 親になるObject
            _root = new GameObject().transform;
            _root.name = "Bullets";
            _root.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        protected override Bullet CreateInstance()
        {
            // インスタンスが新しく必要になったらInstantiateする
            var newBullet = GameObject.Instantiate(_prefab);

            // 親となるTransformを変更する
            newBullet.transform.SetParent(_root);

            return newBullet;
        }
    }
}