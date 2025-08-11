using DG.Tweening;
using DI;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class ShopItemInformer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ShopItemUI _item;

        [Header("Panel")]
        [SerializeField] private GameObject _infoPanel;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _itemDamage;
        [SerializeField] private TextMeshProUGUI _itemAttackSpeed;
        [SerializeField] private TextMeshProUGUI _itemAttackCooldown;
        [SerializeField] private TextMeshProUGUI _itemAttackRange;
        [SerializeField] private TextMeshProUGUI _itemDescription;

        private SoundHandler _soundHandler;

        private void Awake() => 
            _soundHandler = MenuDI.Resolve<SoundHandler>();

        private void Start() => HideInfo();

        public void OnPointerEnter(PointerEventData eventData)
        {
            _soundHandler.PlaySound(ClipName.Selected);

            ShowInfo();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideInfo();
        }

        private void ShowInfo()
        {
            _infoPanel.SetActive(true);

            transform.DOScale(1.05f, 0.3f)
                .SetEase(Ease.InOutCubic)
                .Play();

            _itemDamage.text = $"Damage: {_item.GetItemConfig().InventoryItem.TowerConfig.Damage.ToString()}";
            _itemAttackSpeed.text = $"Speed: {_item.GetItemConfig().InventoryItem.TowerConfig.AttackSpeed.ToString()}";
            _itemAttackCooldown.text = $"Cooldown: {_item.GetItemConfig().InventoryItem.TowerConfig.AttackCoolDown.ToString()}";
            _itemAttackRange.text = $"Range: {_item.GetItemConfig().InventoryItem.TowerConfig.AttackRange.ToString()}";
            _itemDescription.text = _item.GetItemConfig().InventoryItem.TowerGeneral.Description;
        }

        private void HideInfo()
        {
            _infoPanel.SetActive(false);

            transform.DOScale(1f, 0.3f)
                .SetEase(Ease.InOutCubic)
                .Play();
        }
    }
}
