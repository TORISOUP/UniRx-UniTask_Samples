using System;
using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class CreateWithStateSample : MonoBehaviour
    {
        private void Start()
        {
            CreateCountObservable(10).Subscribe(x => Debug.Log(x));
        }

        /// <summary>
        /// 指定個数、連続した値を発行する（Rangeと同じ挙動）Observableを作って返す
        /// </summary>
        /// <param name="count">発行個数</param>
        /// <returns></returns>
        IObservable<int> CreateCountObservable(int count)
        {
            // countが０以下の場合はOnCompletedメッセージのみを返す
            if (count <= 0) return Observable.Empty<int>();

            // 指定した数だけ連続する値を発行する
            return Observable.CreateWithState<int, int>(
                state: count,
                subscribe: (maxCount, observer) =>
                {
                    // 第一引数にstateで指定した値（この場合はcount）が渡される
                    // つまり maxCount = count
                    for (int i = 0; i < maxCount; i++)
                    {
                        observer.OnNext(maxCount);
                    }

                    observer.OnCompleted();
                    return Disposable.Empty;
                });
        }
    }
}