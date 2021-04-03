using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7
{
    public class UniTaskVoidSample2 : MonoBehaviour
    {
        // Start()メソッドをUniTaskVoidにしても問題なく動く
        private async UniTaskVoid Start()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(10));
            Destroy(gameObject);
        }
    }
}