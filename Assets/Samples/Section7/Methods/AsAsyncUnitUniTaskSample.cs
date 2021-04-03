using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Methods
{
    public class AsAsyncUnitUniTaskSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            // UniTask<string>
            var u1 = UniTask.Run(() => File.ReadAllText(@"data.txt"));

            // UniTask
            var u2 = UniTask.Delay(TimeSpan.FromSeconds(1));

            // 結果を返す UniTask.WhenAll は、引数はかならず UniTask<T> でないといけない
            // var (result, _) = await UniTask.WhenAll(u1, u2);

            // AsAsyncUnitUniTaskを使い、 UniTask -> UniTask<AsyncUnit> に変換して型を揃える
            var (result, _) = await UniTask.WhenAll(u1, u2.AsAsyncUnitUniTask());
            Debug.Log(result);
        }
    }
}