using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "Scriptable Object/ShopItems")]
    public class ShopItemSO : ScriptableObject
    {
        public string Name;
        public int Cost;
        public Sprite Sprite;
        public InventoryItemSO InventoryItem;
    }
}
