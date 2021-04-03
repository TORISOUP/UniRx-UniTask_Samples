using System;
using System.IO;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Samples.Section5.AsyncReactiveCommands
{
    public class AsyncReactiveCommandSample4 : MonoBehaviour
    {
        [SerializeField] private Button _buttonA;
        [SerializeField] private Button _buttonB;
        [SerializeField] private Button _buttonC;

        /// <summary>
        /// 制御用のReactiveProperty
        /// </summary>
        private readonly BoolReactiveProperty _sharedGate
            = new BoolReactiveProperty(true);

        private void Start()
        {
            // ButtonA、B、Cはすべて同じReactivePropertyを参照している
            // どれかボタンが押されたら、そのボタンに対応した非同期処理を実行する
            // その非同期処理が終わるまで、押されたボタンを含め他のボタンもすべてInteractive = false状態になる

            // ButtonAが押されたらHTTP通信する
            _buttonA.BindToOnClick(_sharedGate,
                _ =>
                {
                    return FetchAsync("https://unity3d.com/jp")
                        .ToObservable()
                        .ForEachAsync(Debug.Log);
                });

            // ButtonBが押されたらファイルを読み込む
            _buttonB.BindToOnClick(_sharedGate,
                _ =>
                {
                    return Observable
                        .Start(() =>
                        {
                            return File.ReadAllText(@"data.txt");
                        }).ObserveOnMainThread()
                        .ForEachAsync(Debug.Log);
                });

            // ButtonCが押されたら3秒待つ
            _buttonC.BindToOnClick(_sharedGate,
                _ =>
                {
                    return Observable.Timer(TimeSpan.FromSeconds(3))
                        .AsUnitObservable();
                });

            _sharedGate.AddTo(this);
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