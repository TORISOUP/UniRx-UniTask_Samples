using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Subjects.Async
{
    /// <summary>
    /// プレイヤのテクスチャを変更する
    /// </summary>
    public class PlayerTextureChanger2 : MonoBehaviour
    {
        [SerializeField] private GameResourceProvider _gameResourceProvider;

        private readonly CancellationTokenSource _cancellationTokenSource
            = new CancellationTokenSource();

        private void Start()
        {
            var cancellationToken = _cancellationTokenSource.Token;

            // awaitを放置すると警告がでるので、
            // Discards(C#7の機能)で警告を抑制する
            _ = SetMyTextureAsync(cancellationToken);
        }

        private async Task SetMyTextureAsync(CancellationToken token)
        {
            var r = GetComponent<Renderer>();

            var texture = await _gameResourceProvider.PlayerTextureAsync.GetAwaiter(token);

            r.sharedMaterial.mainTexture = texture;
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}