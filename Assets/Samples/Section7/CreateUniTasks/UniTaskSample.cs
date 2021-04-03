using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.CreateUniTasks
{
    public class UniTaskSample : MonoBehaviour
    {
        private void Start()
        {
            // 非同期処理を実行したまま放置したい(Fire-and-forget)場合は、
            // Forget()をコールするとよい
            DoAsync1().Forget();
        }

        /// <summary>
        /// async voidの代替がasync UniTaskVoid
        /// </summary>
        private async UniTaskVoid DoAsync1()
        {
            try
            {
                var result = await DoAsync2();
                Debug.Log(result);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        /// <summary>
        /// UniTaskが結果の値を返す場合はUniTask<T>を使う
        /// </summary>
        private async UniTask<string> DoAsync2()
        {
            try
            {
                await DoAsync3();
                return "OK!";
            }
            catch
            {
                return "Failed!";
            }
        }

        /// <summary>
        /// 返り値をUniTaskにすることでasync/awaitの結果をUniTaskに変換できる
        /// </summary>
        private async UniTask DoAsync3()
        {
            // await対象がTaskでもあっても問題ない
            await Task.Delay(1000);
        }
    }
}