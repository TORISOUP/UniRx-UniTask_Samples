using System;
using UniRx;

namespace Samples.Section3.ReactiveProperty
{
    /// <summary>
    /// フルーツ一覧を表すEnum
    /// </summary>
    public enum Fruit
    {
        Apple,
        Banana,
        Peach,
        Melon,
        Orange
    }

    /// <summary>
    /// Fruit型を扱うReactiveProperty
    /// </summary>
    [Serializable]
    public class FruitReactiveProperty : ReactiveProperty<Fruit>
    {
        public FruitReactiveProperty()
        {
        }

        public FruitReactiveProperty(Fruit init) : base(init)
        {
        }
    }
}