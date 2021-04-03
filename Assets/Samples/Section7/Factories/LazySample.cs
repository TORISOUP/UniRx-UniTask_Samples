using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Factories
{
    public class LazySample : MonoBehaviour
    {
        /// <summary>
        /// テクスチャを遅延初期化で読み込んで返す
        /// </summary>
        public UniTask<Texture> EnemyTextureAsync => _asyncLazy.Task;

        private AsyncLazy<Texture> _asyncLazy;

        private void Awake()
        {
            _asyncLazy = UniTask.Lazy(async () =>
                await Resources.LoadAsync<Texture>("Textures/enemy") as Texture);
        }
    }
}