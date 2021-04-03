using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class RepeatSample : MonoBehaviour
{
    private void Start()
    {
        // Zキーの入力が変化したら通知するObservable
        var zKeyOnChanged = this.UpdateAsObservable()
            .Select(_ => Input.GetKey(KeyCode.Z))
            .DistinctUntilChanged()
            .Skip(1); //Subscribe直後のメッセージを無視するためのSkip

        // Zキーが1秒以上押されていたら通知するObservable
        var zKeyLongPressStart = zKeyOnChanged
            .Throttle(TimeSpan.FromSeconds(1))
            .Where(x => x);

        // Zキーが離された通知するObservable
        var zKeyRelease = zKeyOnChanged.Where(x => !x);

        // 1秒以上Zキーが長押しされている間、
        // メソッドを呼び出す
        this.UpdateAsObservable()
            .SkipUntil(zKeyLongPressStart) // 長押しが始まるまで待つ
            .TakeUntil(zKeyRelease) // キーが離されたら終了
            .Repeat() // TakeUntilが発動したらRepeatで初期状態に戻る
            .Subscribe(_ =>
            {
                OnLongPress();
            }).AddTo(this);
    }

    private void OnLongPress()
    {
        /*
         * 長押し時に実行される処理
         */
    }
}