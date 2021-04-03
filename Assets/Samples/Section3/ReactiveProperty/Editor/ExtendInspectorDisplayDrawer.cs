using UniRx;

namespace Samples.Section3.ReactiveProperty.Editor
{
    //Editor拡張としてこのようなクラスを定義しておく
    [UnityEditor.CustomPropertyDrawer(typeof(FruitReactiveProperty))]
    public class ExtendInspectorDisplayDrawer : InspectorDisplayDrawer
    {
    }
}