using System;
using System.IO;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Schedulers
{
    public class ObserveOnMainThreadSample : MonoBehaviour
    {
        private void Start()
        {
            ReadFileAsync("data.txt")
                // 以降の処理をメインスレッドに切り替える
                .ObserveOnMainThread()
                .Subscribe(x => Debug.Log(x));
        }

        /// <summary>
        /// 指定されたファイルを別スレッド上で読み込む
        /// </summary>
        private IObservable<string> ReadFileAsync(string path)
        {
            return Observable.Start(() =>
            {
                using (var r = new StreamReader(path))
                {
                    return r.ReadToEnd();
                }
            });
        }
    }
}