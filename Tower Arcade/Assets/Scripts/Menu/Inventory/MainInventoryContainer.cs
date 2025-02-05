using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MainInventoryContainer : MonoBehaviour
    {
        [SerializeField] private List<InventorySlot> _inventorySlotHandlers = new List<InventorySlot>();

        public void AddItemToSlot(InventoryItemSO inventoryItem)
        {
            foreach (var slot in _inventorySlotHandlers)
            {
                if (slot.transform.childCount == 0)
                {
                    slot.AddItemToSlop(inventoryItem);
                    break;
                }
            }
        }
    }
}
