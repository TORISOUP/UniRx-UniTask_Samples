using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

class ImplicitConvertSample : MonoBehaviour
{
    private void Start()
    {
    }

    private async UniTaskVoid GetAsync()
    {
        var uri = "https://unity.com/ja";

        var uwr = UnityWebRequest.Get(uri);

        await uwr.SendWebRequest(); // 
    }
}