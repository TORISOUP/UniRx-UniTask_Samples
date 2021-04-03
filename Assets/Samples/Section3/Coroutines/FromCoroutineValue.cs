using System.Collections;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Coroutines
{
    public class FromCoroutineValueSample : MonoBehaviour
    {
        private void Start()
        {
            Observable.FromCoroutineValue<Vector3>(PositionCoroutine, false)
                .Subscribe(
                    x => Debug.Log(x), 
                    () => Debug.Log("OnCompleted"));
        }

        /// <summary>
        /// 座標の配列を順番に発行するイテレータ
        /// </summary>
        private IEnumerator PositionCoroutine()
        {
            var positions = new[]
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 1, 1),
                new Vector3(1, 1, 1),
            };

            foreach (var p in positions)
            {
                yield return p;
            }
        }
    }
}