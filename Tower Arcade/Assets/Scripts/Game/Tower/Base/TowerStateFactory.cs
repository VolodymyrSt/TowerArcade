using UnityEngine;

namespace Game {
    public abstract class TowerStateFactory
    {
        public abstract ITowerState CreateFirstState(Transform parent);
        public abstract ITowerState CreateSecondState(Transform parent);
        public abstract ITowerState CreateThirdState(Transform parent);

        public ITowerState EnterInFirstState(LevelCurencyHandler levelCurencyHandler, Transform parent)
        {
            ITowerState towerState = CreateFirstState(parent);
            towerState.Enter(levelCurencyHandler);
            return towerState;
        }
        public ITowerState EnterInSecondState(LevelCurencyHandler levelCurencyHandler, Transform parent)
        {
            ITowerState towerState = CreateSecondState(parent);
            towerState.Enter(levelCurencyHandler);
            return towerState;
        }

        public ITowerState EnterInThirdtState(LevelCurencyHandler levelCurencyHandler, Transform parent)
        {
            ITowerState towerState = CreateThirdState(parent);
            towerState.Enter(levelCurencyHandler);
            return towerState;
        }
    }
}
