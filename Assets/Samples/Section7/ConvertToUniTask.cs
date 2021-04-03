using System;
using System.Threading.Tasks;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;

class ConvertToUniTask : MonoBehaviour
{
    private void Start()
    {
        // Observable -> UniTask
        var ut1 = Observable.Return(1).ToUniTask();

        // useFirstValueオプションを付けるとTake(1)と同等になる
        var ut2 = Observable.Range(0, 10).ToUniTask(useFirstValue: true);

        // Task -> UniTask
        var ut3 = Task.Run(() => Debug.Log("Do!")).AsUniTask();


        IObservable<AsyncUnit> observable1 = UniTask.Run(() => { }).ToObservable();
        IObservable<string> observable2 = UniTask.Run(() => "Result!").ToObservable();
    }
}