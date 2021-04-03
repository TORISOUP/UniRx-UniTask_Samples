//using System;
//using UnityEngine;
//using System.Collections;
//using System.IO;
//using UniRx;
//
//public class AsUnitObservableSample : MonoBehaviour
//{
//    void Start()
//    {
//        StartCoroutine(ReadFileCoroutine());
//    }
//
//    /// <summary>
//    /// Fileの非同期読み込みをコルーチンで待ち受ける
//    /// </summary>
//    IEnumerator ReadFileCoroutine()
//    {
//        // ToLazyTask() でLazyTaskに変換
//        var lazyTask = ReadFileAsync(@"data.txt").ToLazyTask();
//
//        // Start() を呼び出すとコルーチンに変換される
//        yield return lazyTask.Start();
//
//        if (lazyTask.Status == LazyTask.TaskStatus.Faulted)
//        {
//            // 失敗時はStatusがFaultedになる
//            // 例外はExceptionプロパティで取得できる
//
//            Debug.LogError(lazyTask.Exception);
//        }
//        else if (lazyTask.Status == LazyTask.TaskStatus.Completed)
//        {
//            // 失敗時はStatusがCompletedになる
//            // 結果はResultプロパティで取得できる
//
//            Debug.Log(lazyTask.Result);
//        }
//    }
//
//    /// <summary>
//    /// 非同期でファイルを読み込むIObservableを作成する
//    /// </summary>
//    private IObservable<string> ReadFileAsync(string path)
//    {
//        return Observable.Start(() =>
//        {
//            using (var r = new StreamReader(path))
//            {
//                return r.ReadToEnd();
//            }
//        }, Scheduler.ThreadPool);
//    }
//}

