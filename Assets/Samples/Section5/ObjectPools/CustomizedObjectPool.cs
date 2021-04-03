using UniRx.Toolkit;
using UnityEngine;

namespace Samples.Section5.ObjectPools
{
    public class CustomizedObjectPool<T> : ObjectPool<T> where T : Component
    {
        private T _prefab;

        public CustomizedObjectPool(T prefab)
        {
            this._prefab = prefab;
        }

        /// <summary>
        /// インスタンス作成時に実行される
        /// </summary>
        protected override T CreateInstance()
        {
            return GameObject.Instantiate<T>(_prefab);
        }

        /// <summary>
        /// インスタンスの貸出前に実行される
        /// </summary>
        /// <param name="instance"></param>
        protected override void OnBeforeRent(T instance)
        {
            // base.OnBeforeRent()では
            // instance.gameObject.SetActive(true)を実行している
            base.OnBeforeRent(instance);
        }

        /// <summary>
        /// インスタンスの返却前に実行される
        /// </summary>
        /// <param name="instance"></param>
        protected override void OnBeforeReturn(T instance)
        {
            // base.OnBeforeReturn()では
            // instance.gameObject.SetActive(false)を実行している
            base.OnBeforeReturn(instance);
        }

        /// <summary>
        /// オブジェクト破棄時に実行される
        /// </summary>
        protected override void OnClear(T instance)
        {
            // baseでDestroy()される
            base.OnClear(instance);
        }
    }
}