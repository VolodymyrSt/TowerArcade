using DI;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameInventoryHandler : MonoBehaviour
    {
        public void Initialize(DIContainer container, List<TowerSO> towers)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).TryGetComponent(out GameInventorySlotUI gameInventorySlotUI))
                {
                    gameInventorySlotUI.Initialize(container, towers[i]);
                }
            }
        }
    }
}


