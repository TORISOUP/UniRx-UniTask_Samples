using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

class UniTaskWaitUntil : MonoBehaviour
{
    private Vector3 _initPosition;

    private void Start()
    {
        _initPosition = transform.position;
        CheckPositionAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    /// y座標が0未満になったら初期地点に戻す
    /// </summary>
    private async UniTaskVoid CheckPositionAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await UniTask.WaitUntil(() => transform.position.y < 0, cancellationToken: token);
            transform.position = _initPosition;
        }
    }
}