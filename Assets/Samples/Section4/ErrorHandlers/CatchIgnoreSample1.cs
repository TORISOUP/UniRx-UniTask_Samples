using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.ErrorHandlers
{
    public class CatchIgnoreSample1 : MonoBehaviour
    {
        private void Start()
        {
            var array = new[] {"1", "2", "three", "4"};

            // 例外発生時はログを出力しOnErrorをOnCompletedに差し替える
            array.ToObservable()
                // 文字列からint型に変換する
                .Select(int.Parse)
                .CatchIgnore((ArgumentNullException ex) =>
                {
                    Debug.LogWarning("nullが指定されました");
                })
                .CatchIgnore((FormatException ex) =>
                {
                    Debug.LogWarning("数値ではない文字列が指定されました");
                })
                .Subscribe(
                    x => Debug.Log(x),
                    ex => Debug.LogError(ex),
                    () => Debug.Log("OnCompleted"));
        }
    }
}