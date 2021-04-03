using System;
using UniRx;
using UnityEngine;

namespace Samples.Section4.Synthesizers
{
    public class BatchFrameSample1 : MonoBehaviour
    {
        /// <summary>
        /// 敵オブジェクトの管理をしているコンポーネント（という想定）
        /// </summary>
        [SerializeField] private EnemyManager _enemyManager;

        private void Start()
        {
            // 死んだ敵のIDがObservableで通知される(という想定)
            IObservable<int> killed = _enemyManager.OnKilledEnemyIdObservable;

            // フレーム数0、FrameCountType.EndOfFrameを指定することで、
            // このフレーム中に発生したイベントを1つにまとめることができる
            killed
                .BatchFrame(0, FrameCountType.EndOfFrame)
                .Subscribe(ids =>
                {
                    // 1フレーム中に死亡した敵の数を出力する
                    Debug.Log(Time.frameCount + ":" + ids.Count);
                });
        }
    }

    public class EnemyManager
    {
        // 未実装！
        public IObservable<int> OnKilledEnemyIdObservable { get; }
    }
}