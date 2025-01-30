using UnityEngine;

namespace Game
{
    public class IceStateFactory : TowerStateFactory
    {
        public override ITowerState CreateFirstState(Transform parent)
        {
            var iceTowerFirstStatePrefab = Resources.Load<IceTowerFirstState>("Tower/Ice/State/IceTowerFirstState");
            IceTowerFirstState iceTowerFirstState = Object.Instantiate(iceTowerFirstStatePrefab, parent);
            return iceTowerFirstState;
        }

        public override ITowerState CreateSecondState(Transform parent)
        {
            var iceTowerSecondStatePrefab = Resources.Load<IceTowerSecondState>("Tower/Ice/State/IceTowerSecondState");
            IceTowerSecondState iceTowerSecondState = Object.Instantiate(iceTowerSecondStatePrefab, parent);
            return iceTowerSecondState;
        }

        public override ITowerState CreateThirdState(Transform parent)
        {
            var iceTowerThirdStatePrefab = Resources.Load<IceTowerThirdState>("Tower/Ice/State/IceTowerThirdState");
            IceTowerThirdState iceTowerThirdState = Object.Instantiate(iceTowerThirdStatePrefab, parent);
            return iceTowerThirdState;
        }
    }
}
