using UniRx;
using UnityEngine;

namespace Samples.Section3.Subjects
{
    public class BehaviorSubjectSample : MonoBehaviour
    {
        private void Start()
        {
            // BehaviorSubjectの定義には必ず初期値が必要
            BehaviorSubject<int> behaviorSubject = new BehaviorSubject<int>(0);

            //メッセージを発行
            behaviorSubject.OnNext(1);

            //購読開始
            behaviorSubject.Subscribe(
                x => Debug.Log("OnNext:" + x),
                ex => Debug.LogError("OnError:" + ex),
                () => Debug.Log("OnCompleted"));

            //メッセージを発行
            behaviorSubject.OnNext(2);

            //Valueを参照すると最新の値を確認できる
            Debug.Log("Last Value:" + behaviorSubject.Value);

            behaviorSubject.OnNext(3);
            behaviorSubject.OnCompleted();

            //キャッシュを完全に削除
            behaviorSubject.Dispose();
        }
    }
}