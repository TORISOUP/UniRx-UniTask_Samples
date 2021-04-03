using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Samples.Section7.Awaiter
{
    public class AsyncOperationAwaitSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            try
            {
                // UnityWebRequestAsyncOperationのawait
                var urw = UnityWebRequest.Get("https://unity.com/ja");
                // CancellationTokenを指定
                await urw.SendWebRequest().WithCancellation(token);

                // ToUniTask()を使うと
                // 現在の進行状況(0.0～1.0)の取得とCancellationTokenの指定が同時にできる
                var urw2 = UnityWebRequest.Get("https://unity.com/ja");
                await urw2.SendWebRequest()
                    .ToUniTask(
                        Progress.Create<float>(x => Debug.Log(x)),
                        cancellationToken: token);
            }
            catch (UnityWebRequestException e)
            {
                // isHttpError または isNetworkError 時は
                // UnityWebRequestException が throwされる
                Debug.LogException(e);
            }

            // ResourceRequestのawait
            await Resources.LoadAsync<Texture>("PlayerHealth");

            // AsyncOperationのawait
            await SceneManager.LoadSceneAsync("NextScene");
        }
    }
}