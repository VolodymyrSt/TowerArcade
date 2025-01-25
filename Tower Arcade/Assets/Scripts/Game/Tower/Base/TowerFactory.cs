using UnityEngine;

namespace Game
{
    public abstract class TowerFactory
    {
        public abstract ITower CreateTower();

        public void SpawnTower(Transform spawnPostion, TowerPlacementBlock placementBlock, LevelCurencyHandler levelCurency)
        {
            ITower tower = CreateTower();
            tower.Initialize(levelCurency);

            tower.SetPosition(spawnPostion);
            tower.SetOccupiedBlock(placementBlock);
        }
    }
}
