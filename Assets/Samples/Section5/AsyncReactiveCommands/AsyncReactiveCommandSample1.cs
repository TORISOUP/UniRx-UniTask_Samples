using System;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Samples.Section5.AsyncReactiveCommands
{
    public class AsyncReactiveCommandSample1 : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _resultText;

        private void Start()
        {
            // AsyncReactiveCommand型はAsyncReactiveCommand<Unit>型と同義
            var asyncReactiveCommand = new AsyncReactiveCommand();

            // ボタンを押したらサーバと通信する
            // 通信中はボタンを押せない（通信中はButtonのInteractiveがfalseになる）
            asyncReactiveCommand.BindToOnClick(_button, _ =>
            {
                return FetchAsync("http://api.example.com/status")
                    .ToObservable()
                    .ForEachAsync(x =>
                    {
                        _resultText.text = x; // 結果をUIに表示
                    });
            });
        }

        /// <summary>
        /// UniTaskを使ってサーバ通信
        /// </summary>
        private async UniTask<string> FetchAsync(string url)
        {
            using (var uwr = UnityWebRequest.Get(url))
            {
                await uwr.SendWebRequest();
                if (uwr.isHttpError || uwr.isNetworkError)
                {
                    throw new Exception(uwr.error);
                }

                return uwr.downloadHandler.text;
            }
        }
    }
}