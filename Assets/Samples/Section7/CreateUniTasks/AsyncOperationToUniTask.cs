using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Samples.Section7.CreateUniTasks
{
    public class AsyncOperationToUniTask : MonoBehaviour
    {
        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            // AsyncOperation -> UniTask
            var utask1 = SceneManager.LoadSceneAsync("NextScene")
                .ToUniTask(cancellationToken: token);

            // ResourceRequest -> UniTask
            var utask2 = Resources.LoadAsync<Texture>("Player")
                .ToUniTask(cancellationToken: token);

            // UnityWebRequestAsyncOperation -> UniTask
            var urw = UnityWebRequest.Get("https://unity.com/ja");
            var utask3 = urw.SendWebRequest()
                .ToUniTask(cancellationToken: token);

            // Progress.Create<float> を渡せば
            // 現在の進行状況を取得可能になる (0.0～1.0)
            var urw2 = UnityWebRequest.Get("https://unity.com/ja");
            var utask4 = urw2.SendWebRequest()
                .ToUniTask(
                    Progress.Create<float>(x => Debug.Log(x)),
                    cancellationToken: token);
        }
    }
}