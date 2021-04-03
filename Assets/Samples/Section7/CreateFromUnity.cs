using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

class CreateFromUnity : MonoBehaviour
{
    private void Start3()
    {
        /* AsyncOperationおよびその派生から変換 */

        // AsyncOperation -> UniTask
        var utask1 = SceneManager.LoadSceneAsync("NextScene").ToUniTask();

        // ResourceRequest -> UniTask
        var utask2 = Resources.LoadAsync<Texture>("PlayerHealth").ToUniTask();

        // UnityWebRequestAsyncOperation -> UniTask
        var urw = UnityWebRequest.Get("https://unity.com/ja");
        var utask3 = urw.SendWebRequest().ToUniTask();

        // ConfigureAwaitを使うとUniTaskに変換しつつ、
        // 現在の進行状況を取得可能になる (0.0～1.0)
        var utask4 = urw.SendWebRequest().ToUniTask(Progress.Create<float>(x => { Debug.Log(x); }));
    }

    private void Start()
    {
        // このGameObjectが破棄されたらキャンセルリクエストが発行される
        // CancellationTokenを生成ｓるう
        var cancellationToken = this.GetCancellationTokenOnDestroy();

        // コルーチン -> UniTask
        // ConfigureAwait()を使うことで、CancellationTokenが指定できる
        var fromCoroutineUniTask = Coroutine().ToUniTask(cancellationToken: cancellationToken);
        fromCoroutineUniTask.Forget();
    }

    IEnumerator Coroutine()
    {
        while (true)
        {
            transform.position += Vector3.forward * Time.deltaTime;
            yield return null;
        }
    }
}