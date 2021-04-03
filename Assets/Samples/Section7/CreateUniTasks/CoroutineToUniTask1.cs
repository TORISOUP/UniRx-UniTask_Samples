using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Samples.Section7.CreateUniTasks
{
    public class CoroutineToUniTask1 : MonoBehaviour
    {
        private void Start()
        {
            // コルーチン -> UniTask
            var fromCoroutineUniTask = Coroutine().ToUniTask();
            fromCoroutineUniTask.Forget();
        }

        private IEnumerator Coroutine()
        {
            // 3秒待つ
            yield return new WaitForSeconds(3);

            // シーン遷移する
            yield return SceneManager.LoadSceneAsync("NextScene");

            // 1フレーム待つ
            yield return null;

            Debug.Log("Done!");
        }
    }
}