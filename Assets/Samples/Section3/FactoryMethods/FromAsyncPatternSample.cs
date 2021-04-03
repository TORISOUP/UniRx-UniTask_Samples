using System;
using System.IO;
using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class FromAsyncPatternSample : MonoBehaviour
    {
        private void Start()
        {
            //ファイル名を指定し、Readした結果を返すデリゲート
            Func<string, string> readFile = fileName =>
            {
                using (var r = new StreamReader(fileName))
                {
                    return r.ReadToEnd();
                }
            };

            // デリゲートを非同期実行し、結果をObservableで受け取れるように変換する
            // 返り値もデリゲートになっている
            Func<string, IObservable<string>> f =
                Observable.FromAsyncPattern<string, string>(
                    readFile.BeginInvoke,
                    readFile.EndInvoke);

            // デリゲートを実行したタイミングで非同期処理が開始される
            // 内部でAsyncSubject使っている
            IObservable<string> readAsync = f("data.txt");

            // 結果の購読
            readAsync.Subscribe(x => Debug.Log(x));
        }
    }
}