using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Config", menuName = "Scriptable Object/EnemyConfig")]
    public class EnemyConfigSO : ScriptableObject
    {
        [Header("General")]
        public float MaxHealth;
        public float MoveSpeed;
        public float SoulCost;

        [Header("EnemyDescription")]
        public string EnemyName;
        public string EnemyDescription;
        public EnemyType EnemyType;
        public EnemyRank EnemyRank;

        private void OnValidate()
        {
            MaxHealth = MaxHealth <= 0 ? 1f : MaxHealth;
            MoveSpeed = MoveSpeed <= 0 ? 1f : MoveSpeed;
            SoulCost = SoulCost <= 0 ? 1f : SoulCost;
        }
    }

    public enum EnemyType 
    {
        Normal,
        Slow,
        Speedy,
        Tank,
        Hidden,
        NormalBoss,

        Breaker,
        Necromancer,
        Armored,
        SlowBoss,
        HiddenBoss,
        SpeedyBoss,
    }

    public enum EnemyRank
    {
        E, D, C, B, A, S, SS, SSS
    }
}
