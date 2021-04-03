using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Methods
{
    public class ContinueWithSample : MonoBehaviour
    {
        private void Start()
        {
            // data.txtを読み込んで、１秒後にログに出す
            UniTask
                .Run(() => File.ReadAllText(@"data.txt"))
                .ContinueWith(async x =>
                {
                    // ここの実行コンテキストはメインスレッド
                    Debug.Log(Thread.CurrentThread.ManagedThreadId); // 1

                    // 1秒待ってみる
                    await UniTask.Delay(1000);
                    return x;
                })
                .ContinueWith(x => Debug.Log(x))
                .Forget();
        }
    }
}