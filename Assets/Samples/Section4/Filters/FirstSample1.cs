using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class FirstSample1 : MonoBehaviour
    {
        private void Start()
        {
            // 1から10個連続で数値を発行する
            Observable.Range(1, 10)
                .First(x => x % 3 == 0) //3の約数が来たら通過させて終了
                .Subscribe(
                    x => Debug.Log(x),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}