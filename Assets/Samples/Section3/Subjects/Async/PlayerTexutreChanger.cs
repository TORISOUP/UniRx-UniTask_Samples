using UniRx;
using UnityEngine;

namespace Samples.Section3.Subjects.Async
{
    /// <summary>
    /// プレイヤのテクスチャを変更する
    /// </summary>
    public class PlayerTextureChanger : MonoBehaviour
    {
        [SerializeField] private GameResourceProvider _gameResourceProvider;

        private void Start()
        {
            //プレイヤのテクスチャの読み込みが完了次第テクスチャを変更する
            _gameResourceProvider.PlayerTextureAsync
                .Subscribe(SetMyTexture)
                .AddTo(this);
        }

        private void SetMyTexture(Texture newTexture)
        {
            var r = GetComponent<Renderer>();
            r.sharedMaterial.mainTexture = newTexture;
        }
    }
}