//using UniRx;
//using UnityEngine;
//
//namespace MessageBrokerSample
//{
//    public class Enemy : MonoBehaviour
//    {
//        /// <summary>
//        /// EnemyのID
//        /// </summary>
//        [SerializeField] private int id;
//
//        /// <summary>
//        /// Enemyの体力
//        /// </summary>
//        [SerializeField] private int hp;
//
//        /// <summary>
//        /// メッセージを発行するためのPublisher
//        /// </summary>
//        private IMessagePublisher enemyMessagePublisher
//        {
//            get { return EnemyEventMessageBroker.Default; }
//        }
//
//
//        private void OnCollisionEnter(Collision collision)
//        {
//            // 衝突した対象がPlayerであるか
//            if (collision.gameObject.GetComponent<PlayerHealth>() != null)
//            {
//                // Playerを発見したイベントを通知する
//                enemyMessagePublisher.Publish(new EnemyFoundPlayerEvent(id));
//                return;
//            }
//
//            // 衝突対象がBulletであるか
//            var bullet = collision.gameObject.GetComponent<Bullet>();
//            if (bullet != null)
//            {
//                // ダメージを受けたイベントを通知する
//                enemyMessagePublisher.Publish(new EnemyDamagedEvent(id, bullet.DamageValue));
//                hp -= bullet.DamageValue;
//
//                if (hp <= 0)
//                {
//                    Kill(); //体力がなくなった
//                }
//            }
//        }
//
//        private void Kill()
//        {
//            // やられた通知
//            enemyMessagePublisher.Publish(new EnemyDeadEvent(id));
//            Destroy(gameObject);
//        }
//    }
//}

