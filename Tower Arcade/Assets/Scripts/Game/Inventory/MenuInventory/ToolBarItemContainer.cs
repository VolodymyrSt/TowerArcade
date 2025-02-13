using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ToolBarItemContainer : MonoBehaviour
    {
        private List<TowerSO> _towers;

        public List<TowerSO> GetTowersGeneral()
        {
            _towers = new List<TowerSO>();

            foreach (Transform slot in transform)
            {
                foreach (Transform item in slot)
                {
                    if (item.TryGetComponent(out InventoryItem itemHandler))
                    {
                        itemHandler.gameObject.SetActive(true);
                        _towers.Add(itemHandler.GetTowerGeneral());
                    }
                }
            }

            return _towers;
        }
    }
}
