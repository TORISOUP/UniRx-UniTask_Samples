using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class DisposeStreamSource : MonoBehaviour
    {
        //ストリームソースを定義
        private Subject<int> onChangeHpSubject = new Subject<int>();

        private IObservable<int> OnChanageHpAsObservable
        {
            get { return onChangeHpSubject; }
        }

        void Start()
        {
            //省略
        }

        /// <summary>
        /// OnDestroyでReactivePropertyを明示的に破棄する
        /// </summary>
        void OnDestroy()
        {
            onChangeHpSubject.Dispose();
        }
    }
}