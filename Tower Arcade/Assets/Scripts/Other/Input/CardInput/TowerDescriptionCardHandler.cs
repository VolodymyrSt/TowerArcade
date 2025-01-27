using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TowerDescriptionCardHandler : IUpdatable
    {
        private TowerDescriptionCardUI _towerDescriptionCardUI;
        private LevelCurencyHandler _levelCurencyHandler;
        private Camera _camera;

        private HashSet<RaycastHit> hitsCache = new HashSet<RaycastHit>();

        public TowerDescriptionCardHandler(TowerDescriptionCardUI descriptionCardUI, LevelCurencyHandler levelCurencyHandler)
        {
            _towerDescriptionCardUI = descriptionCardUI;
            _levelCurencyHandler = levelCurencyHandler;
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
                if (!hitsCache.Contains(hit))
                {
                    hitsCache.Add(hit);

                    if (hit.transform.TryGetComponent(out ITowerProperties towerDescription))
                    {
                        _towerDescriptionCardUI.ShowCard();
                        SetUpTowerCard(towerDescription, _levelCurencyHandler);
                    }
                    else return;
                }
                else
                {
                    hitsCache.Remove(hit);
                    _towerDescriptionCardUI.HideCard();
                }
            }
            else return;
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
            towerDescription.TryToUpgradeTower(levelCurencyHandler);
            UpdateTowerCardUI(towerDescription);
        }

        private void DeleteTower(ITowerProperties towerDescription)
        {
            towerDescription.DelateTower();
            _towerDescriptionCardUI.HideCard();
        }

        private void UpdateTowerCardUI(ITowerProperties towerDescription)
        {
            if (towerDescription.IsMaxLevel())
            {
                _towerDescriptionCardUI.SetCardTowerLevel("Max");
                _towerDescriptionCardUI.SetCardTowerUpgradeCost("Max");
            }
            else
            {
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
