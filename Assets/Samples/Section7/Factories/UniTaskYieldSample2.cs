using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Factories
{
    public class UniTaskYieldSample2 : MonoBehaviour
    {
        private void Start()
        {
            YieldAsync().Forget();
        }

        private async UniTaskVoid YieldAsync()
        {
            await UniTask.Yield(PlayerLoopTiming.EarlyUpdate);

            Debug.Log(Time.frameCount);

            await UniTask.Yield(PlayerLoopTiming.Update);

            Debug.Log(Time.frameCount);

            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);

            Debug.Log(Time.frameCount);
            
            // これらすべてのYieldは同一フレーム内で完了する
        }
    }
}