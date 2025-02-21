using UnityEngine;

namespace Game
{
    public class PlacementBlockColorHandler : IUpdatable
    {
        private TowerPlacementBlock _activePlacementBlock;
        private GameInventoryHandler _gameInventoryHandler;

        private Camera _camera;

        public PlacementBlockColorHandler(GameInventoryHandler gameInventoryHandler)
        {
            _camera = Camera.main;
            _gameInventoryHandler = gameInventoryHandler;
        }

        public void Tick() => HandleTowerPlacementBlockSelection();

        private void HandleTowerPlacementBlockSelection()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
            {
                if (hit.transform.TryGetComponent(out TowerPlacementBlock placementBlock))
                {
                    var activeSlot = _gameInventoryHandler.GetActiveSlot();

                    if (placementBlock.IsHighlighted() && activeSlot != null)
                    {
                        if (_activePlacementBlock == null)
                        {
                            Illustrate(placementBlock, activeSlot);
                        }
                        else if (_activePlacementBlock != placementBlock)
                        {
                            _activePlacementBlock.SetSelectedColor(false);
                            _activePlacementBlock.HideIllistaration();

                            Illustrate(placementBlock, activeSlot);
                        }
                    }
                    else return;
                }
                else
                {
                    ClearActivePlacementBlock();
                }
            }
            else
            {
                ClearActivePlacementBlock();
            }
        }

        private void Illustrate(TowerPlacementBlock placementBlock, GameInventorySlotUI activeSlot)
        {
            _activePlacementBlock = placementBlock;
            placementBlock.SetSelectedColor(true);
            placementBlock.ShowIllistaration(activeSlot.Tower.FirstState.AttackRange);
        }

        private void ClearActivePlacementBlock()
        {
            if (_activePlacementBlock != null)
            {
                _activePlacementBlock.SetSelectedColor(false);
                _activePlacementBlock.HideIllistaration();
                _activePlacementBlock = null;
            }
        }
    }
}
