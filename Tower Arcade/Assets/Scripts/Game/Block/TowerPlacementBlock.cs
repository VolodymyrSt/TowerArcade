using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class TowerPlacementBlock : MonoBehaviour
    {
        [SerializeField] private Transform _placePivot;
        [SerializeField] private GameObject _highlightedZone;

        [SerializeField] private GameObject _towerIllustaration;
        [SerializeField] private Transform _towerIllustarationZone;

        [Header("Materials")]
        [SerializeField] private Material _selectedMaterial;
        [SerializeField] private Material _originMaterial;

        private bool _isOccupied = false;
        private bool _isHighlighted = false;

        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            DOTween.Init();

            _meshRenderer = _highlightedZone.GetComponent<MeshRenderer>();

            DisHighlight();

            HideIllistaration();
        }

        public Transform GetPlacePivot() => _placePivot;

        public void SetOccupied(bool value) => _isOccupied = value;

        public bool IsOccupied() => _isOccupied;
        public bool IsHighlighted() => _isHighlighted;

        public void Highlight() 
        { 
            if (!_isOccupied)
            {
                _highlightedZone.SetActive(true);
                _isHighlighted = true;
            }
        }

        public void DisHighlight()
        {
            if (!_isOccupied)
            {
                _isHighlighted = false;
                _highlightedZone.SetActive(false);
            }
        }

        public void SetSelectedColor(bool isSelected)
        {
            if (isSelected) 
                _meshRenderer.material = _selectedMaterial;
            else 
                _meshRenderer.material = _originMaterial;
        }

        public void ShowIllistaration(float zoneRadious)
        {
            _towerIllustaration.SetActive(true);
            _towerIllustarationZone.localScale = new Vector3(zoneRadious, 0.05f, zoneRadious);
        }

        public void HideIllistaration() => 
            _towerIllustaration.SetActive(false);
    }
}
