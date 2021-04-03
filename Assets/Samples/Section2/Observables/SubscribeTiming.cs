using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class SubscribeTiming : MonoBehaviour
    {
        void Start()
        {
            var subject = new Subject<string>();

            //OnNextの内容をスペース区切りで連結し、最後の1つだけを出力するObservable
            var appendStringObservable =
                subject
                    .Scan((previous, current) => previous + " " + current)
                    .Last();


            subject.OnNext("I");
            subject.OnNext("have");

            //途中でSubscribeする
            appendStringObservable.Subscribe(x => Debug.Log(x));

            subject.OnNext("a");
            subject.OnNext("pen.");
            subject.OnCompleted();
        }
    }
}