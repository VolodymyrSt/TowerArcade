using UnityEngine;

namespace Game
{
    public class InventorySlot : BaseInventorySlot
    {
        public void AddItemToSlop(InventoryItemSO inventoryItemConfig)
        {
            var itemPrefab = Resources.Load<InventoryItem>("Inventory/InventoryItem");
            InventoryItem inventoryItem = Instantiate(itemPrefab, transform);

            inventoryItem.SetInventoryItem(inventoryItemConfig);
            inventoryItem.SetSprite(inventoryItemConfig.Sprite);
        }
    }
}
