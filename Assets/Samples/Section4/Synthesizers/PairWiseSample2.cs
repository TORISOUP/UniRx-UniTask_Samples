using UniRx;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    public class PairWiseSample2 : MonoBehaviour
    {
        private void Start()
        {
            //移動した時にその差分を計算する
            transform.ObserveEveryValueChanged(x => x.position)
                .Pairwise((p, c) => c - p)
                .Subscribe(x => Debug.Log("移動した距離:" + x))
                .AddTo(this);
        }
    }
}