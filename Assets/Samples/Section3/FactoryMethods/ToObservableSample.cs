using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class ToObservableSample : MonoBehaviour
    {
        private void Start()
        {
            //文字列の配列
            var strArray = new[] {"apple", "banana", "lemon"};

            //配列からObservableに変換
            strArray.ToObservable()
                .Subscribe(
                    x => Debug.Log("OnNext:" + x),
                    ex => Debug.LogError("OnError:" + ex.Message),
                    () => Debug.Log("OnCompleted")
                );
        }
    }
}