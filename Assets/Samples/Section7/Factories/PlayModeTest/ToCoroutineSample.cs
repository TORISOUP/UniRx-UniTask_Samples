using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Samples.Section7.Factories.PlayModeTest
{
    public class ToCoroutineSample
    {
        // コルーチンしか扱えないUniTestでasync/awaitを使う
        [UnityTest]
        public IEnumerator DelayIgnore() => UniTask.ToCoroutine(async () =>
        {
            var time = Time.realtimeSinceStartup;

            Time.timeScale = 0.5f;
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale: true);

                var elapsed = Time.realtimeSinceStartup - time;
                var totalSeconds = TimeSpan.FromSeconds(elapsed).TotalSeconds;
                
                Assert.AreEqual(3,
                    (int) Math.Round(totalSeconds, MidpointRounding.ToEven));
            }
            finally
            {
                Time.timeScale = 1.0f;
            }
        });
    }
}