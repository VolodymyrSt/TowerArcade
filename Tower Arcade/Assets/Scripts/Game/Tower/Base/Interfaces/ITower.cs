using Game;
using UnityEngine;

public interface ITower
{
    public void Initialize(LevelCurencyHandler levelCurencyHandler);
    public void SetPosition(Transform spawnPosition);
    public void SetOccupiedBlock(TowerPlacementBlock placementBlock);
}
