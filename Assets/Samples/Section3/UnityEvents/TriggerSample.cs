using UniRx;
using UniRx.Triggers; // UniRx.Triggersが必要
using UnityEngine;

namespace Samples.Section3.UnityEvents
{
    public class TriggerSample : MonoBehaviour
    {
        //自分とは別のGameObject
        [SerializeField] private GameObject _childGameObject;

        private void Start()
        {
            // UniRx.Triggersを追加することで、
            // Unityの各イベントをObservableとして扱うことができるようになる

            // 自身のGameObjectでの Update をObservableに変換する
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    transform.position += Vector3.forward * Time.deltaTime;
                });

            // 他のGameObjectの OnCollisionEnter をObservableに変換する
            _childGameObject.OnCollisionEnterAsObservable()
                .Subscribe(collision =>
                {
                    Debug.Log(collision.gameObject.name + "に衝突しました");
                }).AddTo(this); // 他のGameObjectを参照するならAddToを併用したほうが安全

            // 自身のGameObjectの OnDestroy をObservableに変換する
            this.OnDestroyAsObservable()
                .Subscribe(_ =>
                {
                    Debug.Log("破棄されました");
                });
        }
    }
}