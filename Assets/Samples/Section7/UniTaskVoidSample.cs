using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7
{
    public class UniTaskVoidSample : MonoBehaviour
    {
        private void Start()
        {
            // UniTaskVoid は実行完了を待機できない
            // そのため実行後そのまま放置したい時に利用できる
            DoAsync().Forget();
        }

        private async UniTaskVoid DoAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(10));
            Destroy(gameObject);
        }
    }
}