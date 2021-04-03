using System;
using System.Net.Sockets;
using System.Threading;
using Samples.Section7.Methods.SampleImpls;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// UDPでデータを待ち受けるサンプル
/// </summary>
class UdpClientSample : MonoBehaviour
{
    private MyUdpClient _udpClient;

    private void Start()
    {
        _udpClient = new MyUdpClient();
        _udpClient.OnReceived.Subscribe(x => Debug.Log(x.Length));
        _udpClient.AddTo(this);
    }
}
