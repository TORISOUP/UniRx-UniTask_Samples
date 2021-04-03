using System;
using System.Collections;
using System.IO;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Coroutines
{
    public class YieldInstructionSample3 : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(ReadFileCoroutine());
        }

        /// <summary>
        /// Fileの非同期読み込みをコルーチンで待ち受ける
        /// </summary>
        private IEnumerator ReadFileCoroutine()
        {
            // ファイルの非同期読み込みをYieldInstructionに変換する
            // throwOnErrorにfalseを指定すると、
            // 失敗時の例外を保持してくれるようになる
            // (trueの場合は例外がそのままthrowされる）
            var yi = ReadFileAsync(@"data.txt")
                .ToYieldInstruction(throwOnError: false);

            // 待機
            yield return yi;

            if (yi.HasError) //HasErrorで成否の判定ができる
            {
                // OnError時のExceptionはErrorに格納される
                Debug.LogError(yi.Error);
            }
            else
            {
                // 成功時の結果はResultに格納される
                Debug.Log(yi.Result);
            }
        }

        /// <summary>
        /// 非同期でファイルを読み込むIObservableを作成する
        /// </summary>
        private IObservable<string> ReadFileAsync(string path)
        {
            return Observable.Start(() =>
            {
                using (var r = new StreamReader(path))
                {
                    return r.ReadToEnd();
                }
            }, Scheduler.ThreadPool);
        }
    }
}