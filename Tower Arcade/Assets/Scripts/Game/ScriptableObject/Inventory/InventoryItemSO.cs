using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Object/InventoryItems")]
    public class InventoryItemSO : ScriptableObject
    {
        public TowerSO TowerGeneral;
        public TowerConfigSO TowerConfig;
        public Sprite Sprite;
    }
}
