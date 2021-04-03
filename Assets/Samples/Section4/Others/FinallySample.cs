using UnityEngine;
using System.Collections;
using UniRx;

public class FinallySample : MonoBehaviour
{
    void Start()
    {
        Observable.Empty<int>()
            .Finally(() => Debug.Log("finally"))
            .Subscribe(_ => Debug.Log("OnNext"), ex => Debug.LogError("OnError"), () => Debug.Log("OnCompleted"))
            .AddTo(this);
    }
}