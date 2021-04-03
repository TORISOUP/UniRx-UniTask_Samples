using UniRx;
using UnityEngine;

namespace Samples.Section5.MessageBrokers
{
    public class TypedMessageBrokerSample : MonoBehaviour
    {
        private void Start()
        {
            // IEnemyEventについてのみ扱えるMessageBroker
            var enemyMessageBroker = new EnemyEventMessageBroker().AddTo(this);

            enemyMessageBroker
                .Receive<EnemyDeadEvent>()
                .Subscribe(x => Debug.Log(x + ":Enemyがやられた"));

            enemyMessageBroker
                .Publish(new EnemyDeadEvent(1));

            // IPlayerEventについてのみ扱えるMessageBroker
            var playerMessageBroker = new PlayerEventMessageBroker().AddTo(this);

            playerMessageBroker
                .Receive<PlayerDeadEvent>()
                .Subscribe(x => Debug.Log(x + ":Playerがやられた"));

            playerMessageBroker
                .Publish(new PlayerDeadEvent(1));

            // 間違った型を指定するとコンパイルエラーとなって実行ができない

            // playerMessageBroker
            //     .Receive<EnemyDeadEvent>()
            //     .Subscribe(x => Debug.Log(x + ":Enemyがやられた"));

            // enemyMessageBroker.Publish(new PlayerDeadEvent(1));
        }
    }
}