using System;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;

class AsyncReactiveCommandAwaitSample : MonoBehaviour
{
    public BoolReactiveProperty _isEnabled = new BoolReactiveProperty(true);

    private async UniTaskVoid Start()
    {
        var ap = new AsyncReactiveCommand<string>(_isEnabled);

        WaitCommandAsync(ap).Forget();

        ap.Subscribe(_ => { return Observable.Timer(TimeSpan.FromSeconds(2)).AsUnitObservable(); });

        Observable.Timer(TimeSpan.FromSeconds(5))
            .Subscribe(_ => ap.Execute("aaa"));
    }

    private async UniTaskVoid WaitCommandAsync(IAsyncReactiveCommand<string> command)
    {
        // Execute() が実行されたときのパラメータを待ち受ける
        var value = await command;

        // "Work!" が表示される
        Debug.Log(value);

        // ↑はこれと同義
        // command.Take(1).Subscribe(x => Debug.Log(x));
    }
}