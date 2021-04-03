using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Samples.Section7.Awaiter
{
    public class DoTweenAwaitSample : MonoBehaviour
    {
        private void Start()
        {
            MoveAsync().Forget();
        }

        private async UniTaskVoid MoveAsync()
        {
            await transform.DOMove(transform.position + Vector3.up, 1.0f);
            await transform.DOScale(Vector3.one * 2.0f, 1.0f);
        }
    }
}