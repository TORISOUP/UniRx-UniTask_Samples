using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Samples.Section6
{
    internal class ReactiveCommandAwaitSample : MonoBehaviour
    {
        private void Start()
        {
            var isOpen = new BoolReactiveProperty(false);
            var command = new ReactiveCommand<string>(isOpen);

            _ = WaitCommandAsync(command);

            // isOpen = false なので何も起きない
            command.Execute("Not work!");

            // ReactiveCommandを有効化
            isOpen.Value = true;

            // isOpen = true なのでコマンド実行
            command.Execute("Work!");
        }

        private async Task WaitCommandAsync(IReactiveCommand<string> command)
        {
            // Execute() が実行されたときのパラメータを待ち受ける
            var value = await command;

            // "Work!" が表示される
            Debug.Log(value);

            // ↑はこれと同義
            // command.Take(1).Subscribe(x => Debug.Log(x));
        }
    }
}