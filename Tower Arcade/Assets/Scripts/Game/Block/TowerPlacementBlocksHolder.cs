using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TowerPlacementBlocksHolder : MonoBehaviour
    {
        [SerializeField] private List<TowerPlacementBlock> _towerPlacementBlocks = new();

        public List<TowerPlacementBlock> GetAllTowerPlacement() => _towerPlacementBlocks;
        public void AddTowerPlacement(TowerPlacementBlock towerPlacementBlock) => _towerPlacementBlocks.Add(towerPlacementBlock);
        public void RemoveTowerPlacement(TowerPlacementBlock towerPlacementBlock) => _towerPlacementBlocks.Remove(towerPlacementBlock);

        public void TuggleHighlight()
        {
            foreach (var block in _towerPlacementBlocks)
            {
                block.Highlight();
            }
        }
        public void UnTuggleHighlight()
        {
            foreach (var block in _towerPlacementBlocks)
            {
                block.DisHighlight();
            }
        }
    }
}
