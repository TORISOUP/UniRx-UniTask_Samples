using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class WithCurrentThreadSchedulerSample : MonoBehaviour
    {
        private void Start()
        {
            // CurrentThreadSchedulerを指定した場合
            Observable.Range(
                start: 0,
                count: 5,
                scheduler: Scheduler.CurrentThread
            ).Subscribe(x =>
            {
                // 現在のフレーム数とメッセージ値を表示する
                Debug.Log($"frame:{Time.frameCount} value:{x}");
            });
        }
    }
}