using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.Factories
{
    public class UniTaskActionSample : MonoBehaviour
    {
        // デリゲートがあったときに
        private Action _action;
        
        public void Start()
        {
            // デリゲートに非同期関数を登録できる
            _action += UniTask.Action(async () =>
            {
                var result = await ReadFileAsync("@data.txt");
                Debug.Log(result);
            });
            
        }

        /// <summary>
        /// 指定パスのファイルを読み込む
        /// </summary>
        private async UniTask<string> ReadFileAsync(string path)
        {
            return await UniTask.Run<string>((p) => File.ReadAllText((string) p), path);
        }
    }
}