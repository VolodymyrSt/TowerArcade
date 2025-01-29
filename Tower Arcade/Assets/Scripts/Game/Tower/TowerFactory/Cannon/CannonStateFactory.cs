using UnityEngine;

namespace Game
{
    public class CannonStateFactory : TowerStateFactory
    {
        public override ITowerState CreateFirstState(UnityEngine.Transform parent)
        {
            var cannonTowerFirstStatePrefab = Resources.Load<CannonTowerFirstState>("Tower/Cannon/State/CannonTowerFirstState");
            CannonTowerFirstState cannonTowerFirstState = Object.Instantiate(cannonTowerFirstStatePrefab, parent);
            return cannonTowerFirstState;
        }

        public override ITowerState CreateSecondState(UnityEngine.Transform parent)
        {
            var cannonTowerSecondStatePrefab = Resources.Load<CannonTowerSecondState>("Tower/Cannon/State/CannonTowerSecondState");
            CannonTowerSecondState cannonTowerSecondState = Object.Instantiate(cannonTowerSecondStatePrefab, parent);
            return cannonTowerSecondState;
        }

        public override ITowerState CreateThirdState(UnityEngine.Transform parent)
        {
            var cannonTowerThirdStatePrefab = Resources.Load<CannonTowerThirdState>("Tower/Cannon/State/CannonTowerThirdState");
            CannonTowerThirdState cannonTowerThirdState = Object.Instantiate(cannonTowerThirdStatePrefab, parent);
            return cannonTowerThirdState;
        }
    }
}
