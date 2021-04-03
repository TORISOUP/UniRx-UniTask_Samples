using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;

class UniTaskFromSamples : MonoBehaviour
{
    private async UniTaskVoid Start()
    {
    }

    private UniTask<string> ReadFileAsync(string path)
    {
        // 与えられたパスが不正な場合はArgumentExceptionを返す
        if (string.IsNullOrEmpty(path))
        {
            return UniTask.FromException<string>(new ArgumentException("Path is invalid."));
        }

        return UniTask.Run(() => System.IO.File.ReadAllText(path));
    }
}