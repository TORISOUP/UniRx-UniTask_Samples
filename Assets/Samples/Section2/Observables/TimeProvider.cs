using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class PlayerHealth : MonoBehaviour
    {
        /// <summary>
        /// プレイヤが死亡したことを通知するAsyncSubject
        /// </summary>
        public IObservable<Unit> OnPlayerDeadAsync
            => _onPlayerDeadAsyncSubject;

        /// <summary>
        /// プレイヤの死亡通知に利用するストリームソース
        /// </summary>
        private readonly AsyncSubject<Unit> _onPlayerDeadAsyncSubject
            = new AsyncSubject<Unit>();

        /// <summary>
        /// プレイヤの体力
        /// </summary>
        [SerializeField] private int _health = 10;

        /// <summary>
        /// プレイヤにダメージを与える
        /// </summary>
        public void ApplyDamage(int damageValue)
        {
            _health = Math.Max(0, _health - damageValue);

            if (_health == 0)
            {
                _onPlayerDeadAsyncSubject.OnNext(Unit.Default);

                // OnCompletedを発行することで、
                _onPlayerDeadAsyncSubject.OnCompleted();
            }
        }
    }
}