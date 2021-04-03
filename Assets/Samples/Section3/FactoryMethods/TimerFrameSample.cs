using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class TimerFrameSample : MonoBehaviour
    {
        private void Start()
        {
            var timer = Observable.TimerFrame(dueTimeFrameCount: 5);

            Debug.Log("Subscribe()したフレーム:" + Time.frameCount);

            timer.Subscribe(x =>
            {
                Debug.Log("OnNextが発行されたフレーム:" + Time.frameCount);
                Debug.Log("OnNextの中身:" + x);
            }, () => Debug.Log("OnCompleted"));
        }
    }
}