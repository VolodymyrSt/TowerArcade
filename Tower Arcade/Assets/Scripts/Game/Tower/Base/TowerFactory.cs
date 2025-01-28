using UnityEngine;

namespace Game
{
    public abstract class TowerFactory
    {
        public abstract ITower CreateTower();

        public void SpawnTower(Transform spawnPostion, TowerPlacementBlock placementBlock, LevelCurencyHandler levelCurencyHandler, TowerDescriptionCardHandler towerDescriptionCardHandler)
        {
            ITower tower = CreateTower();

            tower.Initialize(levelCurencyHandler, towerDescriptionCardHandler);
            tower.SetPosition(spawnPostion);
            tower.SetOccupiedBlock(placementBlock);
        }
    }
}
