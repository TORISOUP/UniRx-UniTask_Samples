using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Samples.Section4.HotConverters
{
    public class PublishLastSample1 : MonoBehaviour
    {
        /// <summary>
        /// キャッシュ用のObservable
        /// </summary>
        private IObservable<Texture> _cachedPlayerTextureAsync;

        // キャッシュされたObservableがあるならそちらを優先して返す
        // まだ読み込みが開始されていないならここで読み込みを実行する
        public IObservable<Texture> LoadPlayerTextureAsync
            => _cachedPlayerTextureAsync ?? LoadTexture();

        private void Start()
        {
            // テクスチャのロードを開始する
            if (_cachedPlayerTextureAsync == null) LoadTexture();

            _cachedPlayerTextureAsync
                .Subscribe(x => Debug.Log(x))
                .AddTo(this);
        }

        /// <summary>
        /// テクスチャを読み込み非同期で返す
        /// 内部でHot変換するため、呼び出した時点で読み込みが開始される
        /// </summary>
        private IObservable<Texture> LoadTexture()
        {
            // コルーチンをObservableにし、PublishLastでHot変換する
            var connectableObservable = Observable
                .FromCoroutine<Texture>(LoadPlayerTextureCoroutine)
                .PublishLast(); //Hot変換

            // コルーチンの実行およびその結果をキャッシュ用Observableに保存する
            _cachedPlayerTextureAsync = connectableObservable;

            // コルーチンを起動する
            connectableObservable.Connect().AddTo(this);
            return connectableObservable;
        }

        /// <summary>
        /// 非同期でTextureを読み込む
        /// </summary>
        private IEnumerator LoadPlayerTextureCoroutine(IObserver<Texture> observer)
        {
            var r = Resources.LoadAsync<Texture>("Textures/player");

            yield return r;

            observer.OnNext(r.asset as Texture);
            observer.OnCompleted();
        }
    }
}