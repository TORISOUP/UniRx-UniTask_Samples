using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Samples.Section4.StreamConverters
{
    /// <summary>
    /// Destroy時に無限ループを引き起こすコード
    /// </summary>
    public class RepeatSample2 : MonoBehaviour
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
                // キーが離されたらOnCompleted
                .TakeUntil(zKeyInput.Where(x => !x))
                // TakeUntilが発動したら再度Subscribeする
                .Repeat()
                .Subscribe(x => Debug.Log(x + "秒間押されています"));
        }
    }
}