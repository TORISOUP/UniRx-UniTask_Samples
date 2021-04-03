using System.IO;
using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class StartSample : MonoBehaviour
    {
        private void Start()
        {
            // Observable.Start<T>の返り値は IObservable<T>
            Observable.Start(() =>
            {
                // 直接ファイルを読み込む
                using (var r = new StreamReader(@"data.txt"))
                {
                    // 読み取り結果をObservableに流す
                    return r.ReadToEnd();
                }
            }, Scheduler.ThreadPool).Subscribe(x => Debug.Log(x)); //Start()の返り値を直接Subscribeできる
        }
    }
}