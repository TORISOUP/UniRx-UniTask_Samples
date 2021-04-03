using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class HotConvert : MonoBehaviour
    {
        void Start()
        {
            var originalSubject = new Subject<string>();

            //OnNextの内容をスペース区切りで連結し、最後の1つだけを出力するObservable
            //IConnectableObservable<string>になっている
            IConnectableObservable<string> appendStringObservable =
                originalSubject
                    .Scan((previous, current) => previous + " " + current)
                    .Last()
                    .Publish(); //Hot変換するOperator


            //IConnectableObservable.Connect()を呼び出すと内部でSubscribeの実行が走る
            appendStringObservable.Connect();

            originalSubject.OnNext("I");
            originalSubject.OnNext("have");

            //appendStringObservableを直接Subscribeすればよい
            appendStringObservable.Subscribe(x => Debug.Log(x));

            originalSubject.OnNext("a");
            originalSubject.OnNext("pen.");
            originalSubject.OnCompleted();
        }
    }
}