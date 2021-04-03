using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section5.AsyncReactiveCommands
{
    public class AsyncReactiveCommandSample3 : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {
            // 一度押したら3秒間押せないボタン
            // 内部でAsyncReactiveCommandが自動生成される
            _button.BindToOnClick(_ =>
            {
                Debug.Log("Clicked");
                return Observable.Timer(TimeSpan.FromSeconds(3)).AsUnitObservable();
            });
        }
    }
}