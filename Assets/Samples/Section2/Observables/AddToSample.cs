using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class AddToSample : MonoBehaviour
    {
        private void Start()
        {
            // 5フレームごとにメッセージを発行するObservable
            Observable.IntervalFrame(5)
                .Subscribe(_ => Debug.Log("Do!"))
                // このGameObjectのOnDestroyに連動して自動でDispose()させる
                .AddTo(this);
        }
    }
}