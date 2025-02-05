using UnityEngine;

namespace Game
{
    public class InventorySlot : BaseInventorySlot
    {
        public void AddItemToSlop(InventoryItemSO inventoryItem)
        {
            var itemPrefab = Resources.Load<InventoryItem>("Inventory/InventoryItem");
            InventoryItem inventoryItemHandler = Instantiate(itemPrefab, transform);

            inventoryItemHandler.SetInventoryItem(inventoryItem);
            inventoryItemHandler.SetSprite(inventoryItem.Sprite);
            inventoryItemHandler.SetToMain(false);
        }
    }
}
