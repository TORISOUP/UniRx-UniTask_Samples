using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.CreateUniTasks
{
    public class PlayerSample : MonoBehaviour
    {
        /// <summary>
        /// プレイヤが死亡するとCompleteするTask
        /// </summary>
        public UniTask OnPlayerDeadAsync => _uts.Task;

        /// <summary>
        /// UniTaskCompletionSource 本体
        /// </summary>
        private readonly UniTaskCompletionSource _uts = new UniTaskCompletionSource();

        /// <summary>
        /// 死亡フラグ
        /// </summary>
        private bool _isDead;

        /// <summary>
        /// プレイヤを死亡させる
        /// </summary>
        public void Kill()
        {
            if (_isDead) return;
            _isDead = true;
            _uts.TrySetResult(); // UniTaskを完了させる
        }

        public void OnDestroy()
        {
            if (!_isDead) _uts.TrySetCanceled();
        }
    }
}