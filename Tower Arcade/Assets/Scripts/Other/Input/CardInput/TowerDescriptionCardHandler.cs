using Sound;
using UnityEngine;

namespace Game
{
    public class TowerDescriptionCardHandler : IUpdatable
    {
        //Dependencies
        private TowerDescriptionCardUI _towerDescriptionCardUI;
        private LevelCurencyHandler _levelCurencyHandler;
        private EffectPerformer _effectPerformer;
        private MassegeHandlerUI _masegeHandler;
        private SoundHandler _soundHandler;

        private Camera _camera;

        private ITowerProperties _activeTowerPropeties;

        public TowerDescriptionCardHandler(TowerDescriptionCardUI descriptionCardUI, LevelCurencyHandler levelCurencyHandler,
            EffectPerformer effectPerformer, MassegeHandlerUI massegeHandlerUI, SoundHandler soundHandler)
        {
            _towerDescriptionCardUI = descriptionCardUI;
            _levelCurencyHandler = levelCurencyHandler;
            _effectPerformer = effectPerformer;
            _masegeHandler = massegeHandlerUI;
            _soundHandler = soundHandler;

            _camera = Camera.main;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                FillTowerCard();
            }
        }

        private void FillTowerCard()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
            {
                if (hit.transform.TryGetComponent(out ITowerProperties towerPropeties))
                {
                    if (_activeTowerPropeties == towerPropeties)
                    {
                        _soundHandler.PlaySound(ClipName.SoftClick);
                        HideTowerCard();
                    }
                    else
                    {
                        _soundHandler.PlaySound(ClipName.SoftClick);
                        UpdateActiveTower(towerPropeties);
                    }
                }
                else return;
            }
            else return;
        }
        private void HideTowerCard()
        {
            _activeTowerPropeties.TuggleZone(false);
            _towerDescriptionCardUI.HideCard();
            _activeTowerPropeties = null;
        }

        public void UpdateActiveTower(ITowerProperties towerProperties)
        {
            if (_activeTowerPropeties != null)
            {
                _activeTowerPropeties.TuggleZone(false);
            }

            _activeTowerPropeties = towerProperties;
            _towerDescriptionCardUI.ShowCard(_activeTowerPropeties);
            SetUpTowerCard(_activeTowerPropeties, _levelCurencyHandler);
            _activeTowerPropeties.TuggleZone(true);
        }

        private void SetUpTowerCard(ITowerProperties towerDescription, LevelCurencyHandler levelCurencyHandler)
        {
            _towerDescriptionCardUI.ClearButtonListeners();

            _towerDescriptionCardUI.SetUpgradeButtonListener(() => UpgradeTower(towerDescription, levelCurencyHandler));
            _towerDescriptionCardUI.SetDelateButtonListener(() => DeleteTower(towerDescription));

            UpdateTowerCardUI(towerDescription);
        }

        private void UpgradeTower(ITowerProperties towerDescription, LevelCurencyHandler levelCurencyHandler)
        {
            _soundHandler.PlaySound(ClipName.Upgrade);

            towerDescription.TryToUpgradeTower(levelCurencyHandler, _effectPerformer, _masegeHandler);
            UpdateTowerCardUI(towerDescription);
        }

        private void DeleteTower(ITowerProperties towerDescription)
        {
            _soundHandler.PlaySound(ClipName.Delate);

            if (_activeTowerPropeties == towerDescription)
            {
                _activeTowerPropeties = null;
            }

            towerDescription.DelateTower(_effectPerformer);
            _towerDescriptionCardUI.HideCard();
        }

        private void UpdateTowerCardUI(ITowerProperties towerDescription)
        {
            if (towerDescription.IsMaxLevel())
            {
                _towerDescriptionCardUI.SetCardTowerLevel("Max");

                _towerDescriptionCardUI.HideUpgradeRoot();
            }
            else
            {
                _towerDescriptionCardUI.ShowUpgradeRoot();

                _towerDescriptionCardUI.SetCardTowerLevel(towerDescription.GetLevel());
                _towerDescriptionCardUI.SetCardTowerUpgradeCost(towerDescription.GetUpgradeCost());
            }

            _towerDescriptionCardUI.SetCardTowerName(towerDescription.GetName());
            _towerDescriptionCardUI.SetCardTowerDamage(towerDescription.GetDamage());
            _towerDescriptionCardUI.SetCardTowerAttackSpeed(towerDescription.GetAttackSpeed());
            _towerDescriptionCardUI.SetCardTowerAttackCooldown(towerDescription.GetAttackCooldown());
            _towerDescriptionCardUI.SetCardTowerAttackRange(towerDescription.GetAttackRange());
        }
    }
}
