using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

class IEnumeratorAwaiterSample : MonoBehaviour
{
    private async UniTaskVoid Start()
    {
        await MoveCoroutine(Vector3.forward * 1.0f, 2);
        await MoveCoroutine(Vector3.right * 2.0f, 1);
        await MoveCoroutine(Vector3.back * 2.0f, 1);
    }

    /// <summary>
    /// 指定した速度で、指定した秒数移動するコルーチン
    /// </summary>
    private IEnumerator MoveCoroutine(Vector3 velocity, float seconds)
    {
        var start = Time.time;
        while ((Time.time - start) < seconds)
        {
            transform.position += velocity * Time.deltaTime;
            yield return null;
        }
    }
}