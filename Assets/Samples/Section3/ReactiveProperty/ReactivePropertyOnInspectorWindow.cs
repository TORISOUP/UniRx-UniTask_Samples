using UniRx;
using UnityEngine;

namespace Samples.Section3.ReactiveProperty
{
    public class ReactivePropertyOnInspectorWindow : MonoBehaviour
    {
        //ジェネリクス版
        public ReactiveProperty<int> A;

        //Int固定版
        public IntReactiveProperty B;
    }
}