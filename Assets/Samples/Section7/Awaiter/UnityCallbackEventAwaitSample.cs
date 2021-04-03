using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace Samples.Section7.Awaiter
{
    public class UnityCallbackEventAwaitSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            // CancellationToken
            var ct = this.GetCancellationTokenOnDestroy();

            // Awake()の呼び出し待ち
            await this.GetAsyncAwakeTrigger().AwakeAsync();

            // Start()の呼び出し待ち
            await this.GetAsyncStartTrigger().StartAsync();

            // Update系
            await this.GetAsyncUpdateTrigger().UpdateAsync(ct);
            await this.GetAsyncFixedUpdateTrigger().FixedUpdateAsync(ct);
            await this.GetAsyncLateUpdateTrigger().LateUpdateAsync(ct);

            // OnCollision系
            var collisionEnter = await this.GetAsyncCollisionEnterTrigger()
                .OnCollisionEnterAsync(ct);

            var collisionStay = await this.GetAsyncCollisionStayTrigger()
                .OnCollisionStayAsync(ct);

            var collisionExit = await this.GetAsyncCollisionExitTrigger()
                .OnCollisionExitAsync(ct);

            // OnTrigger系
            var collider1 = await this.GetAsyncTriggerEnterTrigger()
                .OnTriggerEnterAsync(ct);

            var collider2 = await this.GetAsyncTriggerStayTrigger()
                .OnTriggerStayAsync(ct);

            var collider3 = await this.GetAsyncTriggerExitTrigger()
                .OnTriggerExitAsync(ct);

            // Animator系
            var layerIndex = await this.GetAsyncAnimatorIKTrigger()
                .OnAnimatorIKAsync(ct);

            await this.GetAsyncAnimatorMoveTrigger().OnAnimatorMoveAsync(ct);

            // Visible系
            await this.GetAsyncBecameVisibleTrigger()
                .OnBecameVisibleAsync(ct);

            await this.GetAsyncBecameInvisibleTrigger()
                .OnBecameInvisibleAsync(ct);

            // Destroy
            await this.GetAsyncDestroyTrigger().OnDestroyAsync();

            // たくさんあるので以下略…
        }
    }
}