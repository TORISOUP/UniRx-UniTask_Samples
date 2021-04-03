using Cysharp.Threading.Tasks;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Samples.Section7.Awaiter
{
    public class JobHandleAwaitSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            var values = new NativeArray<int>(10000, Allocator.TempJob);
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = i;
            }

            var result = await SumAsync(values);

            Debug.Log(result);
        }

        private async UniTask<int> SumAsync(NativeArray<int> values)
        {
            // 初期化
            var results = new NativeArray<int>(1, Allocator.TempJob);

            // ジョブを作成
            var job = new SumJob()
            {
                Result = results,
                Values = values
            };

            var jobHandle = job.Schedule();

            
            // JobHandlerをawait
            await jobHandle;



            // 結果を格納
            var sum = job.Result[0];

            // Dispose
            job.Result.Dispose();
            job.Values.Dispose();

            return sum;
        }

        /// <summary>
        /// 数列を全部足し合わせるJob
        /// </summary>
        private struct SumJob : IJob
        {
            public NativeArray<int> Result;

            public NativeArray<int> Values;

            public void Execute()
            {
                Result[0] = 0;
                for (var i = 0; i < Values.Length; i++)
                {
                    Result[0] += Values[i];
                }
            }
        }
    }
}