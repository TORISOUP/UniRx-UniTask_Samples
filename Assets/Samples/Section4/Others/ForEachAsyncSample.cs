using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Others
{
    public class ForEachAsyncSample : MonoBehaviour
    {
        // テクスチャを非同期で設定するオブジェクト
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _enemy;
        [SerializeField] private GameObject _boss;

        /// <summary>
        /// 初期化完了通知用のAsyncSubject
        /// </summary>
        private readonly AsyncSubject<Unit> _initializedAsyncSubject 
            = new AsyncSubject<Unit>();

        /// <summary>
        /// すべてのオブジェクトの初期化が完了したことを通知する
        /// </summary>
        public IObservable<Unit> OnInitialized => _initializedAsyncSubject;

        private void Start()
        {
            // オブジェクトのテクスチャをまとめて読み込んで設定し、
            // 完了したら初期化完了通知を発行する
            Observable.WhenAll(
                SetTextureAsync(_player, "Textures/_player"),
                SetTextureAsync(_enemy, "Textures/Enemy"),
                SetTextureAsync(_boss, "Textures/Boss")
            ).Subscribe(_initializedAsyncSubject);

            _initializedAsyncSubject.AddTo(this);

            OnInitialized.Subscribe(_ => Debug.Log("初期化が完了しました"));
        }

        /// <summary>
        /// 指定したGameObjectのテクスチャを設定する
        /// </summary>
        private IObservable<Unit> SetTextureAsync(GameObject target, string path)
        {
            return Observable
                .FromCoroutine<Texture>(o => LoadTextureAsync(o, path))
                .ForEachAsync(x =>
                {
                    // 非同期で読み込んだTextureを設定する
                    target.GetComponent<Renderer>().material.mainTexture = x;
                });
        }

        /// <summary>
        /// 非同期でテクスチャを読み込む
        /// </summary>
        private IEnumerator LoadTextureAsync(IObserver<Texture> observer, string path)
        {
            var r = Resources.LoadAsync<Texture>(path);
            yield return r;
            observer.OnNext(r.asset as Texture);
            observer.OnCompleted();
        }
    }
}
