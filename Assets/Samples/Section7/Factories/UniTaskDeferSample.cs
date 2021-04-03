using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Factories
{
    public class UniTaskDeferSample : MonoBehaviour
    {
        private void Start()
        {
            DeferSampleAsync().Forget();
        }

        private async UniTaskVoid DeferSampleAsync()
        {
            // Deferで包むと遅延評価できる
            var uniTask = UniTask.Defer(
                () => LoadTextAsync(@"data.txt"));

            // awaitして初めてLoadTextAsync()が実行される
            await uniTask;
        }

        private UniTask<string> LoadTextAsync(string path)
        {
            Debug.Log("Load");
            return UniTask.Run(() => System.IO.File.ReadAllText(path));
        }
    }
}