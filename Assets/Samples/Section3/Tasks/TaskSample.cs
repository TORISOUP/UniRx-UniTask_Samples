using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Tasks
{
    public class TaskSample : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            // Task<T>
            Task<string> task = Task.Run(() => "Result!");

            // Task<T> -> IObservable<T>
            IObservable<string> task2Observable = task.ToObservable();
            task2Observable.Subscribe();
        }
    }
}