using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class LastSample : MonoBehaviour
    {
        private void Start()
        {
            var array = new[] {1, 3, 4, 7, 2, 5, 9};

            array
                .ToObservable()
                .Last()
                .Subscribe(
                    x => Debug.Log(x),
                    ex => Debug.LogError(ex),
                    () => Debug.Log("OnCompleted"));
        }
    }
}