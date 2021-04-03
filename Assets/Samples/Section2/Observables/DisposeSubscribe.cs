using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class DisposeSubscribe : MonoBehaviour
    {
        void Start()
        {
            var subject = new Subject<int>();

            //同じSubjectを3回Subscribe（3つのObservableが生成される）
            IDisposable DisposableA = subject
                .Subscribe(x => Debug.Log("A:" + x)); //A
            IDisposable DisposableB = subject
                .Subscribe(x => Debug.Log("B:" + x)); //B
            IDisposable DisposableC = subject
                .Subscribe(x => Debug.Log("C:" + x)); //C

            //値を発行
            subject.OnNext(100);

            //AのDisposeを実行する
            DisposableA.Dispose();

            Debug.Log("---");

            //再度値を発行
            subject.OnNext(200);

            //終了（全Observable破棄）
            subject.OnCompleted();
        }
    }
}