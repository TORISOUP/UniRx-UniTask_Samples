using UniRx;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    public class MergeSample1 : MonoBehaviour
    {
        private void Start()
        {
            // Scheduler.MainThreadを指定することで、1フレームに1個ずつ値を発行させる
            var s1 = Observable.Range(10, 3, Scheduler.MainThread);
            var s2 = Observable.Range(20, 3, Scheduler.MainThread);

            // 現在のフレーム数 - 出力結果 をペアにして表示
            s1.Merge(s2)
                .Subscribe(x =>
                {
                    Debug.Log($"{Time.frameCount} - {x}");
                });
        }
    }
}