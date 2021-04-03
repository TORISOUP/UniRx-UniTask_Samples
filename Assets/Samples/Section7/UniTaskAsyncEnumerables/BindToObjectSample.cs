using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables
{
    public class BindToObjectSample : MonoBehaviour
    {
        private class MyClass
        {
            public void Do(int frameCount)
            {
                Debug.Log(frameCount);
            }
        }

        private void Start()
        {
            var myClass = new MyClass();

            // 10フレームに1回、MyClass.Do()を呼びだす
            UniTaskAsyncEnumerable.EveryUpdate()
                .Select((_, i) => i)
                .Where(x => x % 10 == 0)
                .BindTo(
                    myClass,
                    (m, f) => m.Do(f),
                    this.GetCancellationTokenOnDestroy());
        }
    }
}