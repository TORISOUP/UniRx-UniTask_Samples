using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Coroutines
{
    public class FromCoroutineSample3 : MonoBehaviour
    {
        /// <summary>
        /// 長押し判定のしきい値
        /// </summary>
        private readonly float _longPressThresholdSeconds = 1.0f;

        private void Start()
        {
            // 一定時間の長押しを検知するObservable
            Observable.FromCoroutine<bool>(observer => LongPushCoroutine(observer))
                .DistinctUntilChanged() //連続して重複したメッセージを除去
                .Subscribe(x => Debug.Log(x)).AddTo(this);
        }

        /// <summary>
        /// スペースキーの長押しを検知するコルーチン
        /// 一定時間長押しされていたらTrueを返す
        /// キーが離されたらFalseを返す
        /// </summary>
        private IEnumerator LongPushCoroutine(IObserver<bool> observer)
        {
            var isPushed = false;
            var lastPushTime = Time.time;

            while (true)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    if (!isPushed)
                    {
                        //押された直後の時刻を記憶
                        lastPushTime = Time.time;
                        isPushed = true;
                    }
                    else if (Time.time - lastPushTime > _longPressThresholdSeconds)
                    {
                        //一定時間押されているならTrueを発行
                        observer.OnNext(true);
                    }
                }
                else
                {
                    if (isPushed)
                    {
                        //離されたらFalseを発行
                        observer.OnNext(false);
                        isPushed = false;
                    }
                }

                yield return null;
            }
        }
    }
}