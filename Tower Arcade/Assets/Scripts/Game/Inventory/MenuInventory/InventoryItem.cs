using DI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private InventoryItemSO _item;
        [SerializeField] private Image _itemImage;

        [SerializeField] private GameObject _itemStatInformer;

        [Header("Stats")]
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _itemDamage;
        [SerializeField] private TextMeshProUGUI _itemAttackCoolDown;
        [SerializeField] private TextMeshProUGUI _itemAttackSpeed;
        [SerializeField] private TextMeshProUGUI _itemAttackRange;
        [SerializeField] private TextMeshProUGUI _itemDesctiption;

        [SerializeField] private bool _isInMainSlot = false;
        private Transform _originalParent;

        //Dependencies
        private SaveData _saveData;

        private void Awake() => 
            _saveData = MenuDI.Resolve<SaveData>();

        public void Start()
        {
            SetSprite(_item.Sprite);

            if (_saveData.InventoryItems.TryGetValue(_item.TowerConfig.Name, out string slotName))
            {
                GameObject slot = GameObject.Find(slotName);

                if (slot.TryGetComponent(out MainInventorySlot mainInventorySlot))
                    SetToMain(true);
                else
                    SetToMain(false);

                if (slot != null)
                {
                    _originalParent = slot.transform;
                    transform.SetParent(_originalParent, false);
                }
                else
                    _saveData.InventoryItems[_item.TowerConfig.Name] = transform.parent.name;
            }
            else
                _saveData.InventoryItems[_item.TowerConfig.Name] = transform.parent.name;

            _itemStatInformer.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _itemStatInformer.SetActive(true);

            UpdateStats(_item.TowerConfig);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _itemStatInformer.SetActive(false);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_isInMainSlot)
            {
                return;
            }
            else
            {
                _itemImage.raycastTarget = false;

                _originalParent = transform.parent;

                transform.SetParent(transform.root);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isInMainSlot)
                return;
            else
                transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isInMainSlot)
                return;
            else
            {
                _itemImage.raycastTarget = true;

                transform.SetParent(_originalParent);
            }
        }

        public void SetParentAfterDrag(Transform newParent)
        {
            _originalParent = newParent;
            transform.SetParent(newParent, false);

            _saveData.InventoryItems[_item.TowerConfig.Name] = newParent.name;
        }

        public void ReplaceItem(InventoryItem newItem)
        {
            Transform tempParent = _originalParent;
            SetParentAfterDrag(newItem.GetOriginalParent());
            newItem.SetParentAfterDrag(tempParent);
        }

        public Transform GetOriginalParent() => _originalParent;

        public void SetToMain(bool value)
        {
            _itemImage.raycastTarget = true;
            _isInMainSlot = value;
        }
        public bool IsInMainSlot() => _isInMainSlot;


        public void SetSprite(Sprite sprite) => _itemImage.sprite = sprite;
        public void SetInventoryItem(InventoryItemSO inventoryItem) => _item = inventoryItem;

        public TowerSO GetTowerGeneral() => _item.TowerGeneral;

        private void UpdateStats(TowerConfigSO towerConfig)
        {
            _itemName.text = towerConfig.Name;
            _itemDamage.text = $"Damage: {towerConfig.Damage.ToString()}";
            _itemAttackCoolDown.text = $"Cooldown: {towerConfig.AttackCoolDown.ToString()}";
            _itemAttackSpeed.text = $"Speed: {towerConfig.AttackSpeed.ToString()}";
            _itemAttackRange.text = $"Range: {towerConfig.AttackRange.ToString()}";
            _itemDesctiption.text = _item.TowerGeneral.Description;
        }
    }
}
