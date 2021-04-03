using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.ErrorHandlers
{
    public class CatchSample1 : MonoBehaviour
    {
        private void Start()
        {

            var array = new[] {"1", "2", "three", "4"};

            // 「数字の文字列」が想定されている配列をint型に変換していく
            array.ToObservable()
                .Select(int.Parse) // 文字列からint型に変換する。失敗すると例外が出る。
                .Catch((ArgumentNullException ex) =>
                {
                    Debug.LogWarning("nullが指定されました");
                    return Observable.Empty<int>(); // OnCompleted
                })
                .Catch((FormatException ex) =>
                {
                    Debug.LogWarning("数値ではない文字列が指定されました");
                    return Observable.Empty<int>(); // OnCompleted
                })
                .Subscribe(
                    x => Debug.Log(x),
                    ex => Debug.LogError(ex),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}