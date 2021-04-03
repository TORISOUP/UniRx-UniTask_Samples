using UniRx;
using UnityEngine;

namespace Samples.Section4.Filters
{
    public class SkipSample : MonoBehaviour
    {
        private void Start()
        {
            // 1オリジンで10個メッセージを生成
            Observable.Range(1, 10)
                // Subscribe()してから最初の3個を無視
                .Skip(3)
                .Subscribe(x => Debug.Log(x));
        }
    }
}