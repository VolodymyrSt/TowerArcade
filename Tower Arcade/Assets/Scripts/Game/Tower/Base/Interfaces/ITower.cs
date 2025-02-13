namespace Game
{
    public interface ITower
    {
        public void Initialize(LevelCurencyHandler levelCurencyHandler, TowerDescriptionCardHandler towerDescriptionCardHandler);
        public void SetPosition(UnityEngine.Transform spawnPosition);
        public void SetOccupiedBlock(TowerPlacementBlock placementBlock);
    }
}
