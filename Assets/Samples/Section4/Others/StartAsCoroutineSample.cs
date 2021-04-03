using System;
using System.Collections;
using System.IO;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Others
{
    public class StartAsCoroutineSample : MonoBehaviour
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
            // 結果を格納するための変数定義
            var result = default(string);
            var error = default(Exception);

            // StartAsCoroutineでコルーチンを起動すると同時に
            // 成功・失敗時のコールバックを登録する
            yield return ReadFileAsync(@"data.txt")
                .StartAsCoroutine(
                    success => result = success,
                    failure => error = failure
                );

            if (error != null)
            {
                Debug.LogError(error);
            }
            else
            {
                Debug.Log(result);
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