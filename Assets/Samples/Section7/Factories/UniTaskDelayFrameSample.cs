using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Factories
{
    public class UniTaskDelayFrameSample : MonoBehaviour
    {
        /// <summary>
        /// 無敵状態を表すフラグ
        /// </summary>
        private bool _isInvincible;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            ChangeInvincibleAsync(token).Forget();
        }

        /// <summary>
        /// 数フレームだけ無敵フラグを有効にする
        /// </summary>
        private async UniTaskVoid ChangeInvincibleAsync(CancellationToken token)
        {
            _isInvincible = true;
            await UniTask.DelayFrame(3, PlayerLoopTiming.Update, token);
            _isInvincible = false;
        }
    }
}