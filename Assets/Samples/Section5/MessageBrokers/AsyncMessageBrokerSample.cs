using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section5.MessageBrokers
{
    internal class AsyncMessageBrokerSample : MonoBehaviour
    {
        // uGUIのText
        [SerializeField] private Text _text;

        private void Start()
        {
            // AsyncMessageBroker.Default  はアプリケーション全体で有効なインスタンス
            var asyncMessageBroker = AsyncMessageBroker.Default;
            IAsyncMessagePublisher publisher = asyncMessageBroker;
            IAsyncMessageReceiver receiver = asyncMessageBroker;

            // 購読者1
            // 受け取ったstringを3秒間表示した後に消す
            receiver.Subscribe<string>(x =>
            {
                _text.text = x;
                return Observable
                    .Timer(TimeSpan.FromSeconds(3))
                    .ForEachAsync(_ => _text.text = "");
            }).AddTo(this);

            // 購読者2
            // 受け取ったstringをコンソールに出して即終了
            receiver.Subscribe<string>(x =>
            {
                Debug.Log(x);
                return Observable.Return(Unit.Default);
            }).AddTo(this);

            var cancellationToken = this.GetCancellationTokenOnDestroy();
            PublishMessageAsync(publisher, cancellationToken).Forget();
        }

        // メッセージを順番に発行する
        private async UniTaskVoid PublishMessageAsync(
            IAsyncMessagePublisher publisher,
            CancellationToken ct)
        {
            // メッセージを発行する
            // すべての購読者での処理が完了すると次に進む
            await publisher.PublishAsync("Hello").ToUniTask(cancellationToken: ct);
            await publisher.PublishAsync("World").ToUniTask(cancellationToken: ct);
            await publisher.PublishAsync("Bye!").ToUniTask(cancellationToken: ct);
        }
    }
}