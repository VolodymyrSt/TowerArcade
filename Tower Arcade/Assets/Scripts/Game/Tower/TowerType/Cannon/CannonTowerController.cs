using UnityEngine;

namespace Game
{
    public class CannonTowerController : Tower
    {
        public override void Initialize(LevelCurencyHandler levelCurencyHandler)
        {
            LevelCurrencyHandler = levelCurencyHandler;

            var ballistaTowerFirstStatePrefab = Resources.Load<CannonTowerFirstState>("Tower/Cannon/State/CannonTowerFirstState");
            CannonTowerFirstState ballistaTowerFirstState = Instantiate(ballistaTowerFirstStatePrefab, transform);
            ballistaTowerFirstState.Enter(levelCurencyHandler);

            //StateFactory = new BallistaTowerStateFactory();
            //EnterInState(StateFactory.EnterInFirstState(LevelCurrencyHandler, transform));
        }

        public override void UpgradeTower()
        {
            //switch (CurrentLevel)
            //{
            //    case 2:
            //        EnterInState(StateFactory.EnterInSecondState(LevelCurrencyHandler, transform));
            //        break;
            //    case 3:
            //        EnterInState(StateFactory.EnterInThirdtState(LevelCurrencyHandler, transform));
            //        break;
            //    default:
            //        return;
            //}
        }
    }
}
