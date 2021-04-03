using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    public class SwithTest : MonoBehaviour
    {
        void Start()
        {
            // 最後に触れたオブジェクトを追尾する
            this.OnCollisionEnterAsObservable()
                .Select(x => x.gameObject.UpdateAsObservable().Select(_ => x.transform.position))
                .Switch() //新しくできたObservableに切り替え
                .Subscribe(target => { transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime); });
        }
    }
}