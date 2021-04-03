using System.Threading;
using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Samples.Section7.Awaiter
{
    public class UguiAwaitSample_ : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private AsyncUnityEventHandler _asyncUnityEventHandler;

        private void Start()
        {
            // CancellationToken取得
            var token = this.GetCancellationTokenOnDestroy();

            // ButtonクリックのUnityEvent
            UnityEvent clickEvent = _button.onClick;

            // AsyncUnityEventHandlerをキャッシュ
            _asyncUnityEventHandler = clickEvent.GetAsyncEventHandler(token);

            MoveAsync(token).Forget();
        }

        /// <summary>
        /// ボタンがクリックされるたびにオブジェクトを移動させる
        /// </summary>
        private async UniTaskVoid MoveAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                // ボタンがクリックされるのを待つ
                await _asyncUnityEventHandler.OnInvokeAsync();

                // 1m前に進める
                transform.position += Vector3.forward;
            }
        }
    }
}