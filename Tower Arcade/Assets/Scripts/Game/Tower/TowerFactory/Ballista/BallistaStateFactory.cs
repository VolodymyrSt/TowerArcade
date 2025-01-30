using UnityEngine;

namespace Game
{
    public class BallistaStateFactory : TowerStateFactory
    {
        public override ITowerState CreateFirstState(Transform parent)
        {
            var ballistaTowerFirstStatePrefab = Resources.Load<BallistaTowerFirstState>("Tower/Ballista/State/BallistaTowerFirstState");
            BallistaTowerFirstState ballistaTowerFirstState = Object.Instantiate(ballistaTowerFirstStatePrefab, parent);
            return ballistaTowerFirstState;
        }

        public override ITowerState CreateSecondState(Transform parent)
        {
            var ballistaTowerSecondStatePrefab = Resources.Load<BallistaTowerSecondState>("Tower/Ballista/State/BallistaTowerSecondState");
            BallistaTowerSecondState ballistaTowerSecondState = Object.Instantiate(ballistaTowerSecondStatePrefab, parent);
            return ballistaTowerSecondState;
        }

        public override ITowerState CreateThirdState(Transform parent)
        {
            var ballistaTowerThirdStatePrefab = Resources.Load<BallistaTowerThirdState>("Tower/Ballista/State/BallistaTowerThirdState");
            BallistaTowerThirdState ballistaTowerThirdState = Object.Instantiate(ballistaTowerThirdStatePrefab, parent);
            return ballistaTowerThirdState;
        }
    }
}
