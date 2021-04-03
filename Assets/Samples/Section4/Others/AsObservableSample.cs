using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Others
{
    public class AsObservableSample : MonoBehaviour
    {
        private void Start()
        {
            Subject<int> subject = new Subject<int>();

            IObservable<int> observable1 = subject; //アップキャストして変換
            IObservable<int> observable2 = subject.AsObservable(); //AsObservableで変換

            // observable1はsubjectをアップキャストしただけなので、
            // IObserverへクロスキャストして利用することができてしまう
            IObserver<int> observer1 = (IObserver<int>)observable1;
            observer1.OnNext(1);
            observer1.OnCompleted();

            // AsObservableを利用している場合はダウンキャストに失敗する
            // InvalidCastException: Cannot cast from source type to destination type.
            IObserver<int> observer2 = (IObserver<int>)observable2; //例外で失敗する
        }
    }
}