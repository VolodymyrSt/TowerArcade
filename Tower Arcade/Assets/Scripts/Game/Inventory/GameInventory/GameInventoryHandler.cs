using UnityEngine;

namespace Game
{
    public class GameInventoryHandler : MonoBehaviour
    {
        public void Initialize(TowerPlacementBlocksHolder  towerPlacementBlocksHolder)
        {
            foreach(Transform slot in transform)
            {
                if (slot.TryGetComponent(out GameInventorySlotUI gameInventorySlotUI))
                {
                    gameInventorySlotUI.Initialize(towerPlacementBlocksHolder);
                }
            }
        }
    }
}


