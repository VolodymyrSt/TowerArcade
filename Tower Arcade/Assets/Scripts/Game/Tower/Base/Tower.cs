using UnityEngine;

namespace Game
{
    public abstract class Tower : MonoBehaviour, ITower, ITowerProperties
    {
        protected ITowerState CurrentState;
        protected TowerStateFactory StateFactory;
        protected LevelCurencyHandler LevelCurrencyHandler;

        private TowerPlacementBlock _towerPlacementBlock;

        protected int CurrentLevel = 1;
        private int _maxLevel = 3;

        protected string Name;
        protected float AttackDamage;
        protected float AttackSpeed;
        protected float AttackCoolDown;
        protected float AttackRange;
        protected float UpgradeCost;

        public abstract void Initialize(LevelCurencyHandler levelCurencyHandler);
        public void SetPosition(Transform spawnPosition) => transform.SetParent(spawnPosition, false);
        public void SetOccupiedBlock(TowerPlacementBlock placementBlock) => _towerPlacementBlock = placementBlock;

        public int GetLevel() => CurrentLevel;
        public string GetName() => Name;
        public float GetDamage() => AttackDamage;
        public float GetAttackSpeed() => AttackSpeed;
        public float GetAttackCooldown() => AttackCoolDown;
        public float GetAttackRange() => AttackRange;
        public float GetUpgradeCost() => UpgradeCost;

        public void TryToUpgradeTower(LevelCurencyHandler levelCurencyHandler)
        {
            if (levelCurencyHandler.GetCurrentCurrencyCount() >= UpgradeCost)
            {
                if (CurrentLevel == _maxLevel) 
                    return; 
                else
                {
                    CurrentLevel++;
                    levelCurencyHandler.SubtactCurrencyCount(UpgradeCost);
                    UpgradeTower();
                }
            }
        }

        public abstract void UpgradeTower();

        public void DelateTower()
        {
            _towerPlacementBlock.SetOccupied(false);
            Destroy(gameObject);
        }

        public void EnterInState(ITowerState towerState)
        {
            CurrentState?.Exit();

            CurrentState = towerState;
            CurrentState.InitializeStats(ref Name, ref AttackDamage, ref AttackSpeed, ref AttackCoolDown, ref AttackRange, ref UpgradeCost);
        }

        public bool IsMaxLevel() => CurrentLevel == _maxLevel;
    }
}
