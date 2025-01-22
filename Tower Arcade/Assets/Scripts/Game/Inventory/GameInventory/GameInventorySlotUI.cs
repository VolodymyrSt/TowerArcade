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

        [Header("Dependencies")]
        private DIContainer _container;
        private TowerPlacementBlocksHolder _towerPlacementBlocksHolder;
        private TowerFactoryHandler _towerFactoryHandler;
        private Camera _camera;

        private TowerSO _tower;
        private bool _isClickedOnSlot = false;

        public void Initialize(DIContainer container, TowerSO tower)
        {
            _camera = Camera.main;
            _container = container;
            _towerFactoryHandler = container.Resolve<TowerFactoryHandler>();
            _towerPlacementBlocksHolder = container.Resolve<TowerPlacementBlocksHolder>();

            _tower = tower;

            _towerName.text = _tower.TowerName;
            _towerImage.sprite = _tower.TowerSprite;
            _towerSoulCost.text = _tower.SoulCost.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _isClickedOnSlot = true;

            _towerPlacementBlocksHolder.TuggleHighlight();
        }

        private void Update()
        {
            if (_isClickedOnSlot && Mouse.current.leftButton.isPressed)
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue))
                {
                    if (hitInfo.transform.TryGetComponent(out TowerPlacementBlock towerPlacementBlock))
                    {
                        _towerPlacementBlocksHolder.UnTuggleHighlight();

                        _towerFactoryHandler.GetTowerFactoryByType(_container, _tower.TowerType).
                            SpawnTower(towerPlacementBlock.GetPlacePivot());

                        towerPlacementBlock.SetOccupied(true);

                        _isClickedOnSlot = false;
                    }
                    else
                    {
                        _towerPlacementBlocksHolder.UnTuggleHighlight();
                        _isClickedOnSlot = false;
                    }
                }
            }
        }
    }
}
