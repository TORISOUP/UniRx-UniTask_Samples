using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Samples.Section7.CreateUniTasks
{
    public class AwaitUniTaskSample : MonoBehaviour
    {
        private void Start()
        {
            FetchAllAsync().Forget();
        }

        /// <summary>
        /// UnityWebRequestによる通信を同時に実行して
        /// すべて終了するのを待ってから次の処理を実行する
        /// </summary>
        /// <returns></returns>
        private async UniTaskVoid FetchAllAsync()
        {
            var uniTask1 = UnityWebRequest.Get("https://unity.com/ja")
                .SendWebRequest().ToUniTask();

            var uniTask2 = UnityWebRequest.Get("https://github.com/")
                .SendWebRequest().ToUniTask();

            var uniTask3 = UnityWebRequest.Get("https://www.google.com/")
                .SendWebRequest().ToUniTask();

            // 3つのURIに対する通信がすべて終了するのを待つ
            var (r1, r2, r3) = await (uniTask1, uniTask2, uniTask3);

            Debug.Log(r1.responseCode);
            Debug.Log(r2.responseCode);
            Debug.Log(r3.responseCode);
        }
    }
}