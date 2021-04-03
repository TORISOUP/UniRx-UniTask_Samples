using System.IO;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Methods
{
    public class ToObservableSample : MonoBehaviour
    {
        private void Start()
        {
            var uniTask = UniTask.Run(() => File.ReadAllText(@"data.txt"));

            // UniTask<string> -> IObservable<string>
            var observable = uniTask.ToObservable();

            observable.Subscribe(Debug.Log);
        }
    }
}