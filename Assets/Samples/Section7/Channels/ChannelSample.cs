using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.Channels
{
    public class ChannelSample : MonoBehaviour
    {
        private void Start()
        {
            // Channel作成
            var channel = Channel.CreateSingleConsumerUnbounded<int>();

            // Channelを読み取るときはReaderを使う
            var reader = channel.Reader;

            // メッセージの待受
            WaitForChannelAsync(reader, this.GetCancellationTokenOnDestroy()).Forget();

            // 書き込むときはWriteを使う
            var writer = channel.Writer;

            // IObserver<T>.OnNext() に相当
            writer.TryWrite(1);
            writer.TryWrite(2);
            writer.TryWrite(3);

            // IObserver<T>.OnCompleted() に相当
            writer.TryComplete();

            // TryComplete()に例外を渡すと IObserver<T>.OnError() に相当
            writer.TryComplete(new Exception(""));

            // Complete()も存在する
            //
            // こちらはChannelがすでにComplete()されていた場合に
            // ChannelClosedException を発行する

            // Close済みなのでChannelClosedException が発行
            writer.Complete();
        }

        private async UniTaskVoid WaitForChannelAsync(ChannelReader<int> reader, CancellationToken token)
        {
            try
            {

                await reader.ReadAllAsync() // IUniTaskAsyncEnumerable<int>
                    .ForEachAsync(x => Debug.Log(x), token);

                Debug.Log("Done");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}