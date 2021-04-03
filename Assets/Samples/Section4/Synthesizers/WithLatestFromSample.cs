using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    public class WithLatestFromSample : MonoBehaviour
    {
        private void Start()
        {
            var rigidBody = GetComponent<Rigidbody>();

            //入力ストリーム
            var inputStream = this.UpdateAsObservable()
                .Select(_ =>
                {
                    return new Vector3(
                        x: Input.GetAxis("Horizontal"),
                        y: 0,
                        z: Input.GetAxis("Vertical"));
                });

            //FixedUpdateを主軸にし、そこにinputStreamを合成する
            this.FixedUpdateAsObservable()
                .WithLatestFrom(inputStream, (_, input) => input)
                .Subscribe(input =>
                {
                    rigidBody.AddForce(input, ForceMode.Acceleration);
                });
        }
    }
}