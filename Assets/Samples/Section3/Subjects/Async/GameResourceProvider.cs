using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Subjects.Async
{
    /// <summary>
    /// ゲームのリソースを読み込んで管理する
    /// </summary>
    public class GameResourceProvider : MonoBehaviour
    {
        /// <summary>
        /// プレイヤのテクスチャ情報を扱うAsyncSubject
        /// </summary>
        private readonly AsyncSubject<Texture> _playerTextureAsyncSubject
            = new AsyncSubject<Texture>();

        /// <summary>
        /// プレイヤのテクスチャ情報
        /// </summary>
        public IObservable<Texture> PlayerTextureAsync => _playerTextureAsyncSubject;

        private void Start()
        {
            //起動時にテクスチャをロードする
            StartCoroutine(LoadTexture());
        }

        /// <summary>
        /// テクスチャを読み込むコルーチン
        /// </summary>
        private IEnumerator LoadTexture()
        {
            //プレイヤのテクスチャを非同期で読み込み
            var resource = Resources.LoadAsync<Texture>("Textures/player");

            yield return resource;

            //読み込みが完了したらAsyncSubjectで結果を通知する
            _playerTextureAsyncSubject.OnNext(resource.asset as Texture);
            _playerTextureAsyncSubject.OnCompleted();
        }
    }
}