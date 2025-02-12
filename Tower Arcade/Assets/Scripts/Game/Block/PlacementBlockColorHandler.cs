using UnityEngine;

namespace Game
{
    public class PlacementBlockColorHandler : IUpdatable
    {
        private TowerPlacementBlock _activePlacementBlock;

        private Camera _camera;

        public PlacementBlockColorHandler() => _camera = Camera.main;

        public void Tick() => HandleTowerPlacementBlockSelection();

        private void HandleTowerPlacementBlockSelection()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
            {
                if (hit.transform.TryGetComponent(out TowerPlacementBlock placementBlock))
                {
                    if (placementBlock.IsHighlighted())
                    {
                        if (_activePlacementBlock == null)
                        {
                            _activePlacementBlock = placementBlock;
                            placementBlock.SetSelectedColor(true);
                        }
                        else if (_activePlacementBlock != placementBlock)
                        {
                            _activePlacementBlock.SetSelectedColor(false);
                            _activePlacementBlock = placementBlock;
                            placementBlock.SetSelectedColor(true);
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

        private void ClearActivePlacementBlock()
        {
            if (_activePlacementBlock != null)
            {
                _activePlacementBlock.SetSelectedColor(false);
                _activePlacementBlock = null;
            }
        }
    }
}
