using UnityEngine;

namespace Game
{
    public abstract class TowerFactory
    {
        public abstract ITower CreateTower();

        public void SpawnTower(Transform spawnPostion, TowerPlacementBlock placementBlock, LevelCurencyHandler levelCurencyHandler)
        {
            ITower tower = CreateTower();

            tower.Initialize(levelCurencyHandler);
            tower.SetPosition(spawnPostion);
            tower.SetOccupiedBlock(placementBlock);
        }
    }
}
