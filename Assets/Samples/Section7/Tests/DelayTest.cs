using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DelayTest
    {
        [UnityTest]
        public IEnumerator DelayIgnore() => UniTask.ToCoroutine(async () =>
        {
            var time = Time.realtimeSinceStartup;

            Time.timeScale = 0.5f;
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(3), DelayType.Realtime);

                var elapsed = Time.realtimeSinceStartup - time;
                Assert.AreEqual(3,
                    (int) Math.Round(TimeSpan.FromSeconds(elapsed).TotalSeconds, MidpointRounding.ToEven));
            }
            finally
            {
                Time.timeScale = 1.0f;
            }
        });
    }
}