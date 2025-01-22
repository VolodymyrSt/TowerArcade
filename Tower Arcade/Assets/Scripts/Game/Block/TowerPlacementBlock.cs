using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class TowerPlacementBlock : MonoBehaviour
    {
        [SerializeField] private Transform _placePivot;
        [SerializeField] private GameObject _highlightedZone;

        private float _upPosition = 1.1f;
        private float _downPosition = -1f;

        private bool _isOccupied = false;

        private void Awake() => DOTween.Init();

        public Transform GetPlacePivot() => _placePivot;
        public void SetOccupied(bool value) => _isOccupied = value;

        public void Highlight() 
        { 
            if (!_isOccupied) 
                _highlightedZone.transform.DOMoveY(_upPosition, 0.5f).SetEase(Ease.Linear).Play();
        }

        public void DisHighlight()
        {
            if (!_isOccupied)
                _highlightedZone.transform.DOMoveY(_downPosition, 0.5f).SetEase(Ease.InBack).Play();
        }
    }
}
