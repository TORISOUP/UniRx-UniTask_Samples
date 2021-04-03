using System.Threading.Tasks;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.CreateUniTasks
{
    public class ConvertToUniTaskSample
    {
        private void Start()
        {
            // Observable -> UniTask
            var ut1 = Observable.Return(1).ToUniTask();

            // useFirstValueオプションを付けるとTake(1)と同等になる
            var ut2 = Observable.Range(0, 10).ToUniTask(useFirstValue: true);

            // Task -> UniTask
            var ut3 = Task.Run(() => Debug.Log("Do!")).AsUniTask();
        }
    }
}