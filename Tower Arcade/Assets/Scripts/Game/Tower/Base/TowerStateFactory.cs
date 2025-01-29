using UnityEngine;

namespace Game {
    public abstract class TowerStateFactory
    {
        public abstract ITowerState CreateFirstState(UnityEngine.Transform parent);
        public abstract ITowerState CreateSecondState(UnityEngine.Transform parent);
        public abstract ITowerState CreateThirdState(UnityEngine.Transform parent);

        public ITowerState EnterInFirstState(LevelCurencyHandler levelCurencyHandler, UnityEngine.Transform parent)
        {
            ITowerState towerState = CreateFirstState(parent);
            towerState.Enter(levelCurencyHandler);
            return towerState;
        }
        public ITowerState EnterInSecondState(LevelCurencyHandler levelCurencyHandler, UnityEngine.Transform parent)
        {
            ITowerState towerState = CreateSecondState(parent);
            towerState.Enter(levelCurencyHandler);
            return towerState;
        }

        public ITowerState EnterInThirdtState(LevelCurencyHandler levelCurencyHandler, UnityEngine.Transform parent)
        {
            ITowerState towerState = CreateThirdState(parent);
            towerState.Enter(levelCurencyHandler);
            return towerState;
        }
    }
}
