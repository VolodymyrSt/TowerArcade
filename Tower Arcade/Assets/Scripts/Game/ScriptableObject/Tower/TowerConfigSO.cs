using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "Scriptable Object/TowerConfig")]
    public class TowerConfigSO : ScriptableObject
    {
        public float Damage;
        public float AttackSpeed;
        public float AttackCoolDown;
        public float AttackRange;
    }
}
