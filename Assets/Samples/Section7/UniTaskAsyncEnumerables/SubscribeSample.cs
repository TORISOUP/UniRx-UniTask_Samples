using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section7.UniTaskAsyncEnumerables
{
    public class SubscribeSample : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Button _button;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

            // クリックされたらしばらく待ってからInstantiate
            _button.OnClickAsAsyncEnumerable()
                // ここのasync/awaitの完了は待たずに次のMoveNextAsyncが実行される
                .Subscribe(async _ =>
                {
                    await UniTask.Delay(
                        TimeSpan.FromSeconds(3), 
                        cancellationToken: token);
                    
                    Instantiate(_enemyPrefab);
                }, token);

            // 同等の処理をForEachAsyncで書いた場合は
            // UniTask.Void を挟む必要がある
            _button.OnClickAsAsyncEnumerable()
                .ForEachAsync(_ => UniTask.Void(async () =>
                {
                    await UniTask.Delay(
                        TimeSpan.FromSeconds(3), 
                        cancellationToken: token);
                    
                    Instantiate(_enemyPrefab);
                }), token);
        }
    }
}