using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Samples.Section3.StateMachine
{
    public class ObserveStateMachineSample : MonoBehaviour
    {
        private void Start()
        {
            // 対象のAnimator
            var animator = GetComponent<Animator>();

            // ObservableStateMachineTriggerを取得
            var trigger = animator.GetBehaviour<ObservableStateMachineTrigger>();

            // AttackStateに遷移した通知
            var attackStart =
                trigger
                    .OnStateEnterAsObservable()
                    .Where(x => x.StateInfo.IsName("Attack"));

            // AttackStateから遷移する通知
            var attackEnd =
                trigger
                    .OnStateExitAsObservable()
                    .Where(x => x.StateInfo.IsName("Attack"));

            // AttackStateにいる間、毎フレーム処理を実行する
            this.UpdateAsObservable()
                .SkipUntil(attackStart)   // attackStartが発火するまで待つ
                .TakeUntil(attackEnd)     // attackEndが発火したら終わり
                .RepeatUntilDestroy(this) // OnCompletedしたら最初からやり直す
                .Subscribe(_ => { Debug.Log("Attack中！"); })
                .AddTo(this);
        }
    }
}