using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Samples.Section6
{
    public class ObservableAsyncAwait : MonoBehaviour
    {
        private void Start()
        {
            // _ にTaskを代入するとawaitしていないときの
            // コンパイラ警告を抑制できる
            _ = DoAsync();
        }

        private async Task DoAsync()
        {
            // Taskが完了するまで待機
            await Task.Delay(TimeSpan.FromSeconds(1));

            // ObservableがOnCompletedするまで待機
            await Observable.Timer(TimeSpan.FromSeconds(1));

            // 無限に続くObservableを対象とする場合は、Operatorなどを用いて終了条件を追加すること
            await Observable.Interval(TimeSpan.FromSeconds(1)).Take(3);

            // OnNextの値を取得することもできる
            var result = await Observable.Return("Result!");
            Debug.Log(result);

            try
            {
                // await中にOnErrorが発行された場合は例外が飛ぶ
                await Observable.Throw<Unit>(new Exception("Throw exception!"));
            }
            catch (Exception e)
            {
                // try-catchでハンドリング可能
                Debug.LogException(e);
                throw;
            }
        }
    }
}