using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.StreamConverters
{
    public class SampleSample : MonoBehaviour
    {
        void Start()
        {
            // 3秒以内に通信が終わらなかったらTimeout
            ObservableWWW.Get("https://unity3d.com/jp/")
                .Timeout(TimeSpan.FromSeconds(3))
                .Subscribe(x => Debug.Log(x), ex => Debug.LogError(ex));
        }
    }
}