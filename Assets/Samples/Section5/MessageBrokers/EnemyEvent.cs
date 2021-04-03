namespace Samples.Section5.MessageBrokers
{
    /// <summary>
    /// Enemy関係のイベント
    /// </summary>
    public interface IEnemyEvent
    {
        int EnemyId { get; }
    }

    /// <summary>
    /// Enemyがやられたイベント
    /// </summary>
    public struct EnemyDeadEvent : IEnemyEvent
    {
        public int EnemyId { get; private set; }

        public EnemyDeadEvent(int enemyId) : this()
        {
            EnemyId = enemyId;
        }
    }

    // ----------------------- 

    /// <summary>
    /// Player関係のイベント
    /// </summary>
    public interface IPlayerEvent
    {
        int PlayerId { get; }
    }

    /// <summary>
    /// Playerがやられたイベント
    /// </summary>
    public struct PlayerDeadEvent : IPlayerEvent
    {
        public int PlayerId { get; private set; }

        public PlayerDeadEvent(int id) : this()
        {
            PlayerId = id;
        }
    }
}