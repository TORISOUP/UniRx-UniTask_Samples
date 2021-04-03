using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section7.UniTaskAsyncEnumerables.Convert
{
    /// <summary>
    /// InputFieldを待ち受ける例
    /// </summary>
    public class FromUGui : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();

// 連打防止ボタン
// 1回ボタンを押したら2秒間無反応になる
_button.OnClickAsAsyncEnumerable()
    .ForEachAwaitWithCancellationAsync(async (_, ct) =>
    {
        Debug.Log("Clicked!");
        await UniTask.Delay(2000, cancellationToken: ct);
    }, token);
        }
    }
}