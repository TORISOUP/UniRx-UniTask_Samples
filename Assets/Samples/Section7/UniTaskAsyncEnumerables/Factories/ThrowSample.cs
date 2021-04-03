using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Samples.Section7.UniTaskAsyncEnumerables.Factories
{
    public class ThrowSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            try
            {
                await UniTaskAsyncEnumerable
                    .Throw<int>(new Exception("Error!"))
                    .ForEachAsync(x => Debug.Log(x));

                Debug.Log("Done");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}