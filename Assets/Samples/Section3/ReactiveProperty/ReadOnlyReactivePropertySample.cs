using UniRx;
using UnityEngine;

namespace Samples.Section3.ReactiveProperty
{
    public class ReadOnlyReactivePropertySample : MonoBehaviour
    {
        private void Start()
        {
            //int型のReactiveProperty
            var intReactiveProperty = new ReactiveProperty<int>(100);

            //int型のReadOnlyReactiveProperty
            var readOnlyInt =
                intReactiveProperty.ToReadOnlyReactiveProperty(); //ReactivePropertyから変換

            //Readできる
            Debug.Log("現在の値:" + readOnlyInt.Value);

            //購読できる
            readOnlyInt.Subscribe(x => Debug.Log("通知された値:" + x));

            // Writeは実行できない
            // readOnlyInt.Value = 10;
        }
    }
}