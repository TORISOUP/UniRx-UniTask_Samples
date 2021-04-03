using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Linq
{
    public class PublishSample : MonoBehaviour
    {
        private IConnectableUniTaskAsyncEnumerable<AsyncUnit> _connectableUniTaskAsyncEnumerable;
        private IDisposable _disposable;

        private void Start()
        {
            // Publish()の返り値は
            // IConnectableUniTaskAsyncEnumerable<T>
            _connectableUniTaskAsyncEnumerable =
                UniTaskAsyncEnumerable
                    .EveryUpdate().Publish();

            // この時点ではまだ値は発行されない
            _connectableUniTaskAsyncEnumerable
                .ForEachAsync(_ => Debug.Log(Time.frameCount));
        }

        public void Do()
        {
            // Connect() を実行したタイミングで稼働開始
            _disposable = _connectableUniTaskAsyncEnumerable?.Connect();
        }

        private void OnDestroy()
        {
            // Connect()の返り値のDispose()を呼べば停止
            _disposable?.Dispose();
        }
    }
}