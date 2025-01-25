using DI;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class GameInventoryHandler : MonoBehaviour
    {
        private GameInventorySlotUI _activeSlot;

        public void Initialize(DIContainer container, List<TowerSO> towers)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent(out GameInventorySlotUI gameInventorySlotUI))
                {
                    gameInventorySlotUI.Initialize(container, towers[i], this);
                }
            }
        }
        public bool IsSlotActive(GameInventorySlotUI slot) => _activeSlot == slot;

        public void SetActiveSlot(GameInventorySlotUI slot)
        {
            if(_activeSlot != null)
            {
                _activeSlot.UnSelect();
            }

            _activeSlot = slot;
            _activeSlot.SetActive(true);
            _activeSlot.Select();
        }

        public void ClearActiveSlot()
        {
            if (_activeSlot == null) return;

            _activeSlot.SetActive(false);
            _activeSlot.UnSelect();
            _activeSlot = null;
        }

    }
}



