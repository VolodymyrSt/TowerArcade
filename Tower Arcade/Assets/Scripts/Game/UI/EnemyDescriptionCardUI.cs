using TMPro;
using UnityEngine;

namespace Game
{
    public class EnemyDescriptionCardUI : MonoBehaviour
    {
        [SerializeField] private GameObject _card;

        [SerializeField] private TextMeshProUGUI _enemyHealth;
        [SerializeField] private TextMeshProUGUI _enemyName;
        [SerializeField] private TextMeshProUGUI _enemyDescription;
        [SerializeField] private TextMeshProUGUI _enemyType;
        [SerializeField] private TextMeshProUGUI _enemyRank;
        [SerializeField] private TextMeshProUGUI _enemySoulCost;

        private void Awake() => HideCard();

        public void SetCardEnemyHealth(float health) => _enemyHealth.text = health.ToString();
        public void SetCardEnemyName(string name) => _enemyName.text = name;
        public void SetCardEnemyDescription(string description) => _enemyDescription.text = description;
        public void SetCardEnemyType(EnemyType type) => _enemyType.text = type.ToString();
        public void SetCardEnemyRank(EnemyRank rank) => _enemyRank.text = rank.ToString();
        public void SetCardEnemySoulCost(float soulCost) => _enemySoulCost.text = soulCost.ToString();

        public void ShowCard() => _card.SetActive(true);
        public void HideCard() => _card.SetActive(false);
    }
}
