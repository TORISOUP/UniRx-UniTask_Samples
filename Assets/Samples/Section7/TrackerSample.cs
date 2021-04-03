using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TrackerSample : MonoBehaviour
{
    async UniTaskVoid Start()
    {
        await Do();
    }

    private async UniTask Do()
    {
        await UniTask.Run(() => { Thread.Sleep(10000); });
    }
}