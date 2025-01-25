using DI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game
{
    public class GameInventorySlotUI : MonoBehaviour, IPointerClickHandler
    {
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _towerName;
        [SerializeField] private Image _towerImage;
        [SerializeField] private TextMeshProUGUI _towerSoulCost;

        [Header("Background")]
        [SerializeField] private Image _backgraundImage;
        [SerializeField] private Sprite _selectedBackgraundSprite;
        [SerializeField] private Sprite _unselectedBackgraundSprite;

        [Header("Dependencies")]
        private DIContainer _container;
        private TowerPlacementBlocksHolder _towerPlacementBlocksHolder;
        private TowerFactoryHandler _towerFactoryHandler;
        private LevelCurencyHandler _levelCurencyHandler;
        private GameInventoryHandler _gameInventoryHandler;

        private TowerSO _tower;
        private Camera _camera;

        public void Initialize(DIContainer container, TowerSO tower, GameInventoryHandler gameInventoryHandler)
        {
            _camera = Camera.main;

            _container = container;
            _towerFactoryHandler = container.Resolve<TowerFactoryHandler>();
            _towerPlacementBlocksHolder = container.Resolve<TowerPlacementBlocksHolder>();
            _levelCurencyHandler = container.Resolve<LevelCurencyHandler>();
            _gameInventoryHandler = gameInventoryHandler;

            _tower = tower;

            _towerName.text = _tower.TowerName;
            _towerImage.sprite = _tower.TowerSprite;
            _towerSoulCost.text = _tower.SoulCost.ToString();

            UnSelect();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_gameInventoryHandler.IsSlotActive(this))
            {
                _gameInventoryHandler.ClearActiveSlot();
            }
            else
            {
                if (_levelCurencyHandler.GetCurrentCurrencyCount() >= _tower.SoulCost)
                {
                    _gameInventoryHandler.SetActiveSlot(this);
                }
            }
        }

        private void Update()
        {
            if (_gameInventoryHandler.IsSlotActive(this) && Mouse.current.leftButton.isPressed)
            {
                HandleTowerPlacement();
            }
        }

        private void HandleTowerPlacement()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue))
            {
                if (hitInfo.transform.TryGetComponent(out TowerPlacementBlock towerPlacementBlock))
                {
                    _towerFactoryHandler.GetTowerFactoryByType(_container, _tower.TowerType)
                        .SpawnTower(towerPlacementBlock.GetPlacePivot(), towerPlacementBlock, _levelCurencyHandler);

                    _gameInventoryHandler.ClearActiveSlot();

                    towerPlacementBlock.SetOccupied(true);

                    _levelCurencyHandler.SubtactCurrencyCount(_tower.SoulCost);
                }
                else
                {
                    _gameInventoryHandler.ClearActiveSlot();
                }
            }
        }
        public void SetActive(bool isActive)
        {
            if (isActive)
            {
                _towerPlacementBlocksHolder.TuggleHighlight();
            }
            else
            {
                _towerPlacementBlocksHolder.UnTuggleHighlight();
            }
        }

        public void Select() => _backgraundImage.sprite = _selectedBackgraundSprite;
        public void UnSelect() => _backgraundImage.sprite = _unselectedBackgraundSprite;
    }
}
