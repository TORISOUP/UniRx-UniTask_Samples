using UniRx;
using UnityEngine;

namespace Samples.Section3.Subjects
{
    public class SubjectSample : MonoBehaviour
    {
        private void Start()
        {
            var subject = new Subject<int>();

            //メッセージを発行
            subject.OnNext(1);

            //購読開始
            subject.Subscribe(
                x => Debug.Log("OnNext:" + x),
                ex => Debug.LogError("OnError:" + ex),
                () => Debug.Log("OnCompleted"));

            //メッセージを発行
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnCompleted();
            subject.Dispose();
        }
    }
}