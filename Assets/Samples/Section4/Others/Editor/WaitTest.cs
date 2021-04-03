using NUnit.Framework;
using UniRx;

namespace Assets.Samples.Section4.Others.Editor
{
    public class WaitTest
    {
        [Test]
        public void WaitでObservableの終了を待機できる()
        {
            var result = Observable.Return(10).Wait();
            Assert.AreEqual(10, result);
        }

        [Test]
        public void ToArrayと組み合わせてObservableのテストができる()
        {
            var result = Observable.Range(0, 5).ToArray().Wait();
            var expected = new[] {0, 1, 2, 3, 4};

            Assert.AreEqual(expected, result);
        }
    }
}