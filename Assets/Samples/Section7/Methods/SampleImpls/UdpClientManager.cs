using UniRx;
using UnityEngine;

namespace Samples.Section7.Methods.SampleImpls
{
    public class UdpClientManager : MonoBehaviour
    {
        private MyUdpClient _udpClient;

        private void Start()
        {
            _udpClient = new MyUdpClient();
            _udpClient.OnReceived.Subscribe(x => Debug.Log(x.Length));
            _udpClient.AddTo(this);
        }
    }
}