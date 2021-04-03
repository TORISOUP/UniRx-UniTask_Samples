using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class DistinctSample2 : MonoBehaviour
    {
        private void Start()
        {
            this.OnCollisionEnterAsObservable()
                // 過去に衝突したことがあるGameObjectは無視
                .Distinct(x => x.gameObject)
                .Subscribe(x => Debug.Log(x.gameObject.name));
        }
    }
}