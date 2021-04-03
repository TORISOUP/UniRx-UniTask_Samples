using System;
using System.Net.Sockets;
using System.Threading;
using UniRx;
using Cysharp.Threading.Tasks;

namespace Samples.Section7.Methods.SampleImpls
{
    public class MyUdpClient : IDisposable
    {
        public IObservable<byte[]> OnReceived => _subject;

        private readonly Subject<byte[]> _subject;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly int _port;

        public MyUdpClient(int port = 12345)
        {
            _subject = new Subject<byte[]>();
            _cancellationTokenSource = new CancellationTokenSource();
            _port = port;

            ReadAsync(_cancellationTokenSource.Token).Forget();
        }

        /// <summary>
        /// UDPでbyte[]を待ち受ける
        /// </summary>
        private async UniTaskVoid ReadAsync(CancellationToken token)
        {
            // CancellationTokenからUniTaskを生成
            // 返り値は (UniTask, CancellationTokenRegistration)
            var (cancelUniTask, _) = token.ToUniTask();

            using (var client = new UdpClient(_port))
            {
                while (!token.IsCancellationRequested)
                {
                    // UDPでデータを待ち受けるTaskをUniTaskに変換
                    var receiveUniTask = client.ReceiveAsync().AsUniTask();

                    // ReceiveAsyncには外からキャンセルする機能が備わっていない
                    // そこでUniTask.WhenAnyと、CancellationToken.ToUniTaskを併用し、
                    // キャンセルされたらawaitが必ず終了するようにする
                    var (hasResult, result) = await UniTask.WhenAny(receiveUniTask, cancelUniTask);

                    // キャンセルされた場合はfalseになるのでこのまま終了
                    if (!hasResult)
                    {
                        return;
                    }

                    // 結果を出力
                    _subject.OnNext(result.Buffer);
                }
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _subject.OnCompleted();
            _subject.Dispose();
        }
    }
}