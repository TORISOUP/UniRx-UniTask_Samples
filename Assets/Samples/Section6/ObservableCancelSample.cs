using System;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Samples.Section6
{
    public class ObservableCancelSample : MonoBehaviour
    {
        private IDisposable _disposable;

        private void Start()
        {
            _disposable =
                Observable
                    .Start(() => Thread.Sleep(1000))
                    .Subscribe(_ => Debug.Log("Done"));
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}