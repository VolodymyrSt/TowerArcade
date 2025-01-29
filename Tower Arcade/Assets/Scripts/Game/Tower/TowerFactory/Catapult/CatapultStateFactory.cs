using UnityEngine;

namespace Game
{
    public class CatapultStateFactory : TowerStateFactory
    {
        public override ITowerState CreateFirstState(UnityEngine.Transform parent)
        {
            var catapultTowerFirstStatePrefab = Resources.Load<CatapultTowerFirstState>("Tower/Catapult/State/CatapultTowerFirstState");
            ITowerState catapultTowerFirstState = Object.Instantiate(catapultTowerFirstStatePrefab, parent);
            return catapultTowerFirstState;
        }

        public override ITowerState CreateSecondState(UnityEngine.Transform parent)
        {
            var catapultTowerSecondStatePrefab = Resources.Load<CatapultTowerSecondState>("Tower/Catapult/State/CatapultTowerSecondState");
            ITowerState catapultTowerSecondState = Object.Instantiate(catapultTowerSecondStatePrefab, parent);
            return catapultTowerSecondState;
        }

        public override ITowerState CreateThirdState(UnityEngine.Transform parent)
        {
            var catapultTowerThirdStatePrefab = Resources.Load<CatapultTowerThirdState>("Tower/Catapult/State/CatapultTowerThirdState");
            ITowerState catapultTowerThirdState = Object.Instantiate(catapultTowerThirdStatePrefab, parent);
            return catapultTowerThirdState;
        }
    }
}
