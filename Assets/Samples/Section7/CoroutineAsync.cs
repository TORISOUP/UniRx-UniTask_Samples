using System.Collections;
using UniRx;
using Cysharp.Threading.Tasks;
//not use UniRx.Async
using UnityEngine;

public class CoroutineAsync : MonoBehaviour
{
    private async UniTaskVoid Start()
    {
        await CoroutineSample();
    }

    IEnumerator CoroutineSample()
    {
        Debug.Log(Time.frameCount);

        yield return null;

        Debug.Log(Time.frameCount);

        yield return new WaitForSeconds(1);

        Debug.Log(Time.frameCount);
    }
}