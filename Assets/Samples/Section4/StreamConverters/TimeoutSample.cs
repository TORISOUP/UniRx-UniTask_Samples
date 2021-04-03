using System;
using System.IO;
using UniRx;
using UnityEngine;

namespace Samples.Section4.StreamConverters
{
    public class TimeoutSample : MonoBehaviour
    {
        private void Start()
        {
            // ファイルを非同期に読み込む
            Observable.Start(() => File.ReadAllText(@"data.txt"))
                // 1秒以内に完了しなければタイムアウト
                .Timeout(TimeSpan.FromSeconds(1))
                .Subscribe(
                    x => Debug.Log(x),
                    ex => Debug.LogError(ex)
                );
        }
    }
}