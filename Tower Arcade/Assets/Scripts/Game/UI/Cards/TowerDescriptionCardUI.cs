using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class TowerDescriptionCardUI : MonoBehaviour
    {
        [SerializeField] private GameObject _card;

        [Header("Buttons:")]
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _delateButton;

        [Header("Text:")]
        [SerializeField] private TextMeshProUGUI _towerNameText;
        [SerializeField] private TextMeshProUGUI _towerLevelText;
        [SerializeField] private TextMeshProUGUI _towerDamageText;
        [SerializeField] private TextMeshProUGUI _towerAttackSpeedText;
        [SerializeField] private TextMeshProUGUI _towerAttackRangeText;
        [SerializeField] private TextMeshProUGUI _towerAttackCooldownText;
        [SerializeField] private TextMeshProUGUI _towerUpgradeCostText;

        private void Awake() => HideCard();

        public void SetUpgradeButtonListener(Action update) => _upgradeButton.onClick.AddListener(() => update.Invoke());
        public void SetDelateButtonListener(Action delate) => _delateButton.onClick.AddListener(() => delate.Invoke());
        public void ClearButtonListeners()
        {
            _upgradeButton?.onClick.RemoveAllListeners();
            _delateButton?.onClick.RemoveAllListeners();
        }

        public void SetCardTowerName(string name) => _towerNameText.text = name;
        public void SetCardTowerLevel(int level) => _towerLevelText.text = $"Level: {level.ToString()}";
        public void SetCardTowerLevel(string level) => _towerLevelText.text = $"Level: {level}";
        public void SetCardTowerDamage(float damage) => _towerDamageText.text = $"Damage: {damage.ToString()}";
        public void SetCardTowerAttackSpeed(float attackSpeed) => _towerAttackSpeedText.text = $"Speed: {attackSpeed.ToString()}";
        public void SetCardTowerAttackCooldown(float attackCooldown) => _towerAttackCooldownText.text = $"Cooldown: {attackCooldown.ToString()}";
        public void SetCardTowerAttackRange(float attackRange) => _towerAttackRangeText.text = $"Range: {attackRange.ToString()}";
        public void SetCardTowerUpgradeCost(float upgradeCost) => _towerUpgradeCostText.text = upgradeCost.ToString();
        public void SetCardTowerUpgradeCost(string upgradeCost) => _towerUpgradeCostText.text = upgradeCost;

        public void ShowCard() => _card.SetActive(true);
        public void HideCard() => _card.SetActive(false);
    }
}
