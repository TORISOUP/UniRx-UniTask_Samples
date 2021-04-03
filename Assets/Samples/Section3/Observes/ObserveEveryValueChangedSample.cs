using UniRx;
using UnityEngine;

namespace Samples.Section3.Observes
{
    public class ObserveEveryValueChangedSample : MonoBehaviour
    {
        private void Start()
        {
            //毎フレーム座標を監視して、変化したら通知する
            transform.ObserveEveryValueChanged(x => x.position)
                .Subscribe(vec3 => Debug.Log("現在の座標:" + vec3));

            var rigidBody = GetComponent<Rigidbody>();

            // FixedUpdate基準でRigidBodyを監視し、速度が変化したら通知する
            // FrameCountTypeでどのフレームを基準にするかを指定できる
            rigidBody
                .ObserveEveryValueChanged(
                    x => x.velocity,
                    FrameCountType.FixedUpdate)
                .Subscribe(vec3 => Debug.Log("速度:" + vec3));
        }
    }
}