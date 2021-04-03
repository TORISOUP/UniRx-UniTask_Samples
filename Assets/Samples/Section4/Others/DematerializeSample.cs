using UniRx;
using UnityEngine;

namespace Samples.Section4.Others
{
    public class DematerializeSample : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(0, 3)
                .Materialize()
                .Dematerialize()
                .Subscribe(x => Debug.Log(x));
        }
    }
}