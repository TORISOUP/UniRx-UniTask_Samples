using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class DisposeStreamSource2 : MonoBehaviour
    {
        //ストリームソースを定義
        private Subject<int> onChangeHpSubject = new Subject<int>();

        private IObservable<int> OnChanageHpAsObservable
        {
            get { return onChangeHpSubject; }
        }

        void Start()
        {
            onChangeHpSubject.AddTo(this); // DestroyされたらSubject.Dispose()を実行する
        }
    }
}