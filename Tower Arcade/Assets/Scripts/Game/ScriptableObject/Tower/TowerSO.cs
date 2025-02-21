using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Tower", menuName = "Scriptable Object/Tower")]
    public class TowerSO : ScriptableObject
    {
        public string TowerName;
        public Sprite TowerSprite;
        public TowerFactoryType TowerType;
        public float SoulCost;
        public string Description;
        public TowerConfigSO FirstState;
    }
}
