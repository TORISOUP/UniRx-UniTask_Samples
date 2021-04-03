using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Methods
{
    public class ForgetSample : MonoBehaviour
    {
        private void Start()
        {
            // asyncメソッドはawaitしないとコンパイラに警告される
            DoAsync(); // 警告 CS4014

            // Forget()をつけることで、警告を抑制できる
            DoAsync().Forget();
        }

        private async UniTaskVoid DoAsync()
        {
            await UniTask.Delay(1000);
        }
    }
}