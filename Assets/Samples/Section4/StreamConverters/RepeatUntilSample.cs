using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Samples.Section4.StreamConverters
{
    public class RepeatUntilSample : MonoBehaviour
    {
        private void Start()
        {
            // Zキーの状態を通知するObservable
            var zKeyInput = this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.Z));

            // Zキーが押されている間、押され始めてからの経過時刻を通知する
            zKeyInput
                .Where(x => x)
                .Select(_ => Time.deltaTime)
                .Scan((p, c) => p + c)
                .TakeUntil(zKeyInput.Where(x => !x))
                //　このgameObjectがDestroyされたら
                //　OnCompletedメッセージを発行して終了する
                .RepeatUntilDestroy(this)
                .Subscribe(x => Debug.Log(x + "秒間押されています"));
        }
    }
}