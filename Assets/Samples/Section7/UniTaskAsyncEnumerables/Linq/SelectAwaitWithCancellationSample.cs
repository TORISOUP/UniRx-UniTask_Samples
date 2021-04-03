using Cysharp.Threading.Tasks.Linq;

namespace Samples.Section7.UniTaskAsyncEnumerables.Linq
{
    public class SelectAwaitWithCancellationSample
    {
        private void Start()
        {
            UniTaskAsyncEnumerable.Range(0, 10)
                .SelectAwaitWithCancellation(async (x, token) =>
                {
                    return 1;
                });
        }
    }
}