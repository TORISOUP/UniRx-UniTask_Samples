using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Samples.Section6
{
    public class ReactivePropertyAwaitSample : MonoBehaviour
    {
        public readonly BoolReactiveProperty _isDeadReactiveProperty
            = new BoolReactiveProperty(false);

        private void Start()
        {
            _ = CheckHealthChangeAsync();
        }

        /// <summary>
        /// 死亡フラグが有効になったらGameObjectを削除する
        /// </summary>
        private async Task CheckHealthChangeAsync()
        {
            await _isDeadReactiveProperty;
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            // Disposeすることで、Awaiterごと破棄される（awaitが中断される）
            _isDeadReactiveProperty.Dispose();
        }
    }
}