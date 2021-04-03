using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class WithMainThreadSchedulerSample : MonoBehaviour
    {
        private void Start()
        {
            // MainThreadSchedulerを指定した場合
            Observable.Range(
                start: 0,
                count: 5,
                scheduler: Scheduler.MainThread
            ).Subscribe(x =>
            {
                // 現在のフレーム数とメッセージ値を表示する
                Debug.Log($"frame:{Time.frameCount} value:{x}");
            });
        }
    }
}