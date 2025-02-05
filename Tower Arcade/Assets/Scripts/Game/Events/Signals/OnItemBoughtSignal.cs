using Game;
using UnityEngine;

public class OnItemBoughtSignal
{
    public InventoryItemSO Item;
    public OnItemBoughtSignal(InventoryItemSO itemSO)
    {
        Item = itemSO;
    }
}
