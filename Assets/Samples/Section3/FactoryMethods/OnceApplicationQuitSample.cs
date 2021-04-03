using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class OnceApplicationQuitSample : MonoBehaviour
    {
        private void Start()
        {
            Observable.OnceApplicationQuit()
                .Subscribe(_ => Debug.Log("アプリケーションが終了しました"));
        }
    }
}