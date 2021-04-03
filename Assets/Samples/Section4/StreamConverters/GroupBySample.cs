using UniRx;
using UnityEngine;

namespace Samples.Section4.StreamConverters
{
    public class GroupBySample : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(1, 10)
                .GroupBy(x => x % 2)
                .Subscribe(groupedObservable =>
                {
                    if (groupedObservable.Key == 0)
                    {
                        // Even Observable
                        groupedObservable
                            .Subscribe(x =>
                            {
                                Debug.Log("Even: " + x);
                            });
                    }
                    else
                    {
                        // Odd Observable
                        groupedObservable
                            .Subscribe(x =>
                            {
                                Debug.Log("Odd: " + x);
                            });
                    }
                });
        }
    }
}