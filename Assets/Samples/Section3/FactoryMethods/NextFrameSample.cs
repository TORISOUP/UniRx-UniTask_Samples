using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class NextFrameSample : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            //スペースキーが押されたら、RigidBodyに力を加える
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //次のFixedUpdateのタイミングで処理を実行する
                Observable.NextFrame(FrameCountType.FixedUpdate)
                    .Subscribe(_ =>
                    {
                        _rigidbody.AddForce(Vector3.up, ForceMode.VelocityChange);
                    });
            }
        }
    }
}