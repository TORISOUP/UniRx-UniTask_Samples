using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.ErrorHandlers
{
    public class CatchIgnoreSample2 : MonoBehaviour
    {
        private void Start()
        {
            var array = new[] { "1", "2", "three", "4" };

            array.ToObservable()
                .Select(int.Parse) // 文字列からint型に変換する
                .CatchIgnore() // 例外を処理せずにOnCompletedに変換する
                .Subscribe(
                    x => Debug.Log(x),
                    ex => Debug.LogError(ex),
                    () => Debug.Log("OnCompleted"));
        }
    }
}