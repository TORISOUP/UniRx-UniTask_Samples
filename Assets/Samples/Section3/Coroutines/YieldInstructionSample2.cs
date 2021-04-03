using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section3.Coroutines
{
    public class YieldInstructionSample2 : MonoBehaviour
    {
        /// <summary>
        /// uGUIのButton
        /// </summary>
        [SerializeField] private Button _moveButton;

        private void Start()
        {
            StartCoroutine(MoveCoroutine());
        }

        /// <summary>
        /// uGUIのButtonが押されたら1秒間オブジェクトを前進させるコルーチン
        /// </summary>
        private IEnumerator MoveCoroutine()
        {
            while (true)
            {
                // Buttonが押されるまで待つ
                // OnClickAsObservable()は無限長ストリームなので、Take(1)で長さを1に制限する
                yield return _moveButton.OnClickAsObservable().Take(1).ToYieldInstruction();

                var start = Time.time;
                while (Time.time - start <= 1.0f)
                {
                    transform.position += Vector3.forward * Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}