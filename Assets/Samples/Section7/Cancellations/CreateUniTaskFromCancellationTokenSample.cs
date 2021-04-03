using System;
using UnityEngine;
using System.Collections;
using System.Threading;
using UniRx;
using Cysharp.Threading.Tasks;

using UniRx.Triggers;

public class CreateUniTaskFromCancellationTokenSample : MonoBehaviour
{
    private void Start()
    {
        var cts = new CancellationTokenSource();

        var uniTask = cts.Token.ToUniTask();

        this.UpdateAsObservable()
            .Subscribe(_ => { Debug.Log(uniTask.Item1.Status); });

        Observable.Timer(TimeSpan.FromSeconds(2))
            .Subscribe(_ => cts.Cancel());
    }
}