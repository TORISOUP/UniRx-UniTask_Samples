using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Samples.Section4.StreamConverters
{
    public class RepeatSafeSample : MonoBehaviour
    {
        private void Start()
        {
            // Zキーの状態を通知するObservable
            var zKeyInput = this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.Z));

            // Zキーが離されたことを通知するObservable
            var trigger = this.UpdateAsObservable()
                .Where(_ => Input.GetKeyUp(KeyCode.Z));

            // Zキーが押されている間、押され始めてからの経過時刻を通知する
            zKeyInput
                .Where(x => x)
                .Select(_ => Time.deltaTime)
                .Scan((p, c) => p + c)
                .TakeUntil(trigger)
                // OnCompletedの無限ループが発生しても
                // RepeatSafeなら安全
                .RepeatSafe()
                .Subscribe(x => Debug.Log(x + "秒間押されています"))
                .AddTo(this); //GameObjectが破棄されたら確実にDispose()する
        }
    }
}