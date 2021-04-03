using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class ThrottleFirstSample : MonoBehaviour
    {
        private void Start()
        {
            // Zキーが押しっぱなしのときに0.5秒間隔でメッセージを発行する
            this.UpdateAsObservable()
                .Where(_ => Input.GetKey(KeyCode.Z))
                // 1メッセージ通過のたびに0.5秒間遮断する
                .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
                .Subscribe(_ => Debug.Log("Press Z key."));
        }
    }
}