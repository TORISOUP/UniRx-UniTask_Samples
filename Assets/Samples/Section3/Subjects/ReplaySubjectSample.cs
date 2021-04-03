using UniRx;
using UnityEngine;

namespace Samples.Section3.Subjects
{
    public class ReplaySubjectSample : MonoBehaviour
    {
        private void Start()
        {
            // 過去3メッセージまでキャッシュするReplaySubject
            var subject = new ReplaySubject<int>(bufferSize: 3);

            //メッセージを発行
            for (int i = 0; i < 10; i++)
            {
                subject.OnNext(i);
            }

            // OnCompletedメッセージもキャッシュされる
            subject.OnCompleted();

            // OnErrorメッセージもキャッシュできる
            // subject.OnError(new Exception("Error!"));

            // 購読
            subject.Subscribe(
                x => Debug.Log("OnNext:" + x),
                ex => Debug.LogError("OnError:" + ex),
                () => Debug.Log("OnCompleted"));

            subject.Dispose();
        }
    }
}