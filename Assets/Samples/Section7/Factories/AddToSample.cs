using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Factories
{
    public class AddToSample : MonoBehaviour
    {
        public IReadOnlyAsyncReactiveProperty<int> Current => _current;
        private AsyncReactiveProperty<int> _current;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            // AsyncReactiveProperty生成
            _current = new AsyncReactiveProperty<int>(0);

            // AsyncReactivePropertyのDispose()呼び出しを
            // CancellationTokenと紐付ける
            _current.AddTo(token);
        }
    }
}