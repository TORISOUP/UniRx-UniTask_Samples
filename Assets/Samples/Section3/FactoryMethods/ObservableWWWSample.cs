using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class ObservableWWWSample : MonoBehaviour
    {
        private void Start()
        {
            var url = "https://unity3d.com/jp";

            //結果(www.text)のみを受け取る場合
            ObservableWWW.Get(url)
                .Subscribe(x => Debug.Log(x));

            //通信終了時にwwwごと受け取る場合
            ObservableWWW.GetWWW(url)
                .Subscribe(www => Debug.Log(www.text));
        }
    }
}