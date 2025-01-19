using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game
{
    public class GameInventorySlotUI : MonoBehaviour, IPointerClickHandler
    {
        private TowerPlacementBlocksHolder _towerPlacementBlocksHolder;

        [SerializeField] private TowerSO _tower;

        private bool _isClickedOnSlot = false;
        private Camera _camera;

        public void Initialize(TowerPlacementBlocksHolder blocksHolder)
        {
            _camera = Camera.main;
            _towerPlacementBlocksHolder = blocksHolder;
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
                        var tower = Instantiate(_tower.TowerPrefab);
                        tower.transform.SetParent(towerPlacementBlock.GetPlacePivot(), false);

                        _towerPlacementBlocksHolder.UnTuggleHighlight();
                        _towerPlacementBlocksHolder.RemoveTowerPlacement(towerPlacementBlock);
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

        public void SetTowerToSlot(TowerSO tower) => _tower = tower;
    }
}
