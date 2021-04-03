using System.Collections;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Coroutines
{
    public class FromCoroutineSample : MonoBehaviour
    {
        private void Start()
        {
            // コルーチンの終了をObservableで待ち受ける
            Observable.FromCoroutine(WaitingCoroutine, publishEveryYield: false)
                .Subscribe(
                    _ => Debug.Log("OnNext"), 
                    () => Debug.Log("OnCompleted"))
                .AddTo(this);

            // ToObservable() という省略記法もある
            //WaitingCoroutine().ToObservable()
            //    .Subscribe();
        }

        // 3秒待つだけのコルーチン
        private IEnumerator WaitingCoroutine()
        {
            Debug.Log("Coroutine start.");

            //3秒間待機
            yield return new WaitForSeconds(3);

            Debug.Log("Coroutine finish.");
        }
    }
}