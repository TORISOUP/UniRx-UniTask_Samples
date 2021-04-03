using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section7.Factories
{
    public class UniTaskUnityActionSample : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private InputField _pathInputField;

        public void Start()
        {
            _button
                .onClick
                .AddListener(UniTask.UnityAction(async () =>
                {
                    var path = _pathInputField.text;
                    var result = await ReadFileAsync(path);
                    Debug.Log(result);
                }));
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