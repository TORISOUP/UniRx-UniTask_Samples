using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section4.StreamConverters
{
    public class SampleObservable : MonoBehaviour
    {
        [SerializeField] private Button ANext;
        [SerializeField] private Button ACom;
        [SerializeField] private Button NNext;
        [SerializeField] private Button NComp;

        void Start()
        {
            var sa = new Subject<string>();
            var sn = new Subject<int>();

            var c1 = 'a';
            var ci = 0;
            ANext.OnClickAsObservable().Subscribe(_ => sa.OnNext(((char) (c1 + ci++)).ToString()));
            ACom.OnClickAsObservable().Subscribe(_ => sa.OnCompleted());

            var ni = 0;
            NNext.OnClickAsObservable().Subscribe(_ => sn.OnNext(ni++));
            NComp.OnClickAsObservable().Subscribe(_ => sn.OnCompleted());

            sa.Sample(sn).Subscribe(x => { Debug.Log(x); }, () => Debug.Log("OnCompleted"));
        }
    }
}