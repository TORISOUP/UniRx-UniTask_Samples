using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.AsyncReactiveProperties
{
    public class AsyncReactivePropertyWithoutCurrentSample : MonoBehaviour
    {
        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            // AsyncReactiveProperty生成
            var asyncReactiveProperty = new AsyncReactiveProperty<string>("Initialize!");

            // WithoutCurrent()を挟むと現在値の発行をスキップする
            asyncReactiveProperty
                .WithoutCurrent()
                .ForEachAsync(x => Debug.Log(x), token);

            asyncReactiveProperty.Value = "Hello!";
            asyncReactiveProperty.Dispose();
            
        }
    }
}