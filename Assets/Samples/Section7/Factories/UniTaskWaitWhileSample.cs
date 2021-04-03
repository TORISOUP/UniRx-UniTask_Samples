using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Factories
{
    public class UniTaskWaitWhileSample : MonoBehaviour
    {
        private void Start()
        {
            CheckPositionAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        /// <summary>
        /// y座標が0未満になったら削除する
        /// </summary>
        private async UniTaskVoid CheckPositionAsync(CancellationToken token)
        {
            await UniTask.WaitWhile(
                () => transform.position.y >= 0, 
                cancellationToken: token);
            
            Destroy(gameObject);
        }
    }
}