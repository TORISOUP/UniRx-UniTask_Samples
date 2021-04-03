using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Factories
{
    public class UniTaskCreateSample : MonoBehaviour
    {
        public void Start()
        {
            // 120フレーム間前進したらDestroyする
            DestroyAsync(UniTask.Create(async () =>
            {
                // 120フレームの間前進する
                for (int i = 0; i < 120; i++)
                {
                    transform.position += Vector3.forward * Time.deltaTime;
                    await UniTask.Yield();
                }
            })).Forget();
        }

        /// <summary>
        /// 指定の処理が終わったらDestroyする
        /// </summary>
        private async UniTaskVoid DestroyAsync(UniTask asyncAction)
        {
            await asyncAction;
            Destroy(gameObject);
        }
    }
}