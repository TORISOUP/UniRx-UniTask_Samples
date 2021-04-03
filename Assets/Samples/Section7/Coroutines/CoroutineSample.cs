using System.Collections;
using UnityEngine;

namespace Samples.Section7.Coroutines
{
    public class SimpleCoroutine : MonoBehaviour
    {
        private void Start()
        {
            // 特定のパターンでオブジェクトを移動させる
            StartCoroutine(MovePatternCoroutine());
        }

        /// <summary>
        /// 特定のパターンで移動するコルーチン
        /// </summary>
        private IEnumerator MovePatternCoroutine()
        {
            yield return MoveCoroutine(Vector3.up * 2.0f, 1.0f);
            yield return new WaitForSeconds(1);

            yield return MoveCoroutine(Vector3.down * 1.0f, 3.0f);
            yield return new WaitForSeconds(0.5f);

            yield return MoveCoroutine(Vector3.right * 4.0f, 2.0f);
            yield return new WaitForSeconds(2);
        }

        /// <summary>
        /// 指定した速度で指定秒数移動するコルーチン
        /// </summary>
        private IEnumerator MoveCoroutine(Vector3 velocity, float seconds)
        {
            var startTime = Time.time;
            while (Time.time - startTime < seconds)
            {
                transform.position += velocity * Time.deltaTime;
                yield return null;
            }
        }
    }
}