using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace Samples._6._2
{
    class TriggerSample : MonoBehaviour
    {
        private void Start()
        {
            AAA().Forget();
        }

        private async UniTaskVoid AAA()
        {
            var trigger = this.GetAsyncCollisionEnterTrigger();
        }
    }
}