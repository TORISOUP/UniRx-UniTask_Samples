using UniRx;
using UnityEngine;

namespace Samples.Section2.Operators
{
    /// <summary>
    /// メッセージをフィルタリングするサンプル
    /// </summary>
    public class OperatorTest : MonoBehaviour
    {
        void Start()
        {
            var subject = new Subject<int>();

            // そのままSubscribe
            subject.Subscribe(x => Debug.Log("raw:" + x));

            // 0以下を除外してSubscribe
            subject.Where(x => x > 0).Subscribe(x => Debug.Log("filter:" + x));

            // メッセージ発行
            subject.OnNext(1);
            subject.OnNext(-1);
            subject.OnNext(3);
            subject.OnNext(0);

            // 終了
            subject.OnCompleted();
        }
    }
}