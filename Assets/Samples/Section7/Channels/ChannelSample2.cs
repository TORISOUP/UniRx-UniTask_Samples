using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.Channels
{
    public class ChannelSample2 : MonoBehaviour
    {
        private void Start()
        {
            // Channel作成
            var channel = Channel.CreateSingleConsumerUnbounded<int>();

            // ReadAsync + Publish で何回も待受可能になる
            var connectable = channel.Reader.ReadAllAsync().Publish();

            using (connectable.Connect())
            {
                var cancellationToken = this.GetCancellationTokenOnDestroy();

                // 複数回待受
                WaitForChannelAsync(connectable, cancellationToken).Forget();
                WaitForChannelAsync(connectable, cancellationToken).Forget();
                WaitForChannelAsync(connectable, cancellationToken).Forget();

                var writer = channel.Writer;

                writer.TryWrite(1);
                writer.TryWrite(2);
                writer.TryWrite(3);
                writer.Complete();
            }
        }

        private async UniTaskVoid WaitForChannelAsync(IUniTaskAsyncEnumerable<int> reader, CancellationToken token)
        {
            try
            {
                await reader // IUniTaskAsyncEnumerable<int>
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