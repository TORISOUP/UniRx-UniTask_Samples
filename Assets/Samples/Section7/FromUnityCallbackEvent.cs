using System.Threading;
using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

class FromUnityCallbackEvent : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Start()
    {
        var unityEvent = new UnityEvent();
        UniTask uniTask = unityEvent.OnInvokeAsync(CancellationToken.None);
    }
}