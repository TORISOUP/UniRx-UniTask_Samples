using UniRx;
using UnityEngine;

namespace Samples.Section4.Others
{
    public class MaterialSample : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(0, 3)
                .Materialize()
                .Subscribe(x => Debug.Log(x));
        }
    }
}