using System;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Samples.Section4.Synthesizers
{
    public class SelectManySample1 : MonoBehaviour
    {
        //ダウンロードボタン
        [SerializeField] private Button _downloadButton;

        //URI入力欄
        [SerializeField] private InputField _urlInputField;

        private void Start()
        {
            // uGUIのボタンがクリックされたら、指定のURLに対してHTTP通信を行う
            _downloadButton.OnClickAsObservable()
                .Select(_ => _urlInputField.text) // 入力されたURLを取得
                .SelectMany(url => FetchAsync(url).ToObservable()) //指定URLに通信する
                .Subscribe(x => Debug.Log(x));
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