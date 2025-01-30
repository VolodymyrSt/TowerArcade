using UnityEngine;

namespace Game
{
    public class FireStateFactory : TowerStateFactory
    {
        public override ITowerState CreateFirstState(Transform parent)
        {
            var fireTowerFirstStatePrefab = Resources.Load<FireTowerFirstState>("Tower/Fire/State/FireTowerFirstState");
            FireTowerFirstState fireTowerFirstState = Object.Instantiate(fireTowerFirstStatePrefab, parent);
            return fireTowerFirstState;
        }

        public override ITowerState CreateSecondState(Transform parent)
        {
            var fireTowerSecondStatePrefab = Resources.Load<FireTowerSecondState>("Tower/Fire/State/FireTowerSecondState");
            FireTowerSecondState fireTowerSecondState = Object.Instantiate(fireTowerSecondStatePrefab, parent);
            return fireTowerSecondState;
        }

        public override ITowerState CreateThirdState(Transform parent)
        {
            var fireTowerThirdStatePrefab = Resources.Load<FireTowerThirdState>("Tower/Fire/State/FireTowerThirdState");
            FireTowerThirdState fireTowerThirdState = Object.Instantiate(fireTowerThirdStatePrefab, parent);
            return fireTowerThirdState;
        }
    }
}
