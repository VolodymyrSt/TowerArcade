using UnityEngine;

public abstract class TowerFactory
{
    public abstract ITower CreateTower();

    public void SpawnTower(Transform spawnPostion)
    {
        ITower tower = CreateTower();
        tower.Initialize();

        tower.SetPosition(spawnPostion);
    }
}
