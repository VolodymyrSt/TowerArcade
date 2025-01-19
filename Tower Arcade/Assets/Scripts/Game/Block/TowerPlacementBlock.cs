using UnityEngine;

namespace Game
{
    public class TowerPlacementBlock : MonoBehaviour
    {
        [SerializeField] private Transform _placePivot;
        [SerializeField] private GameObject _highlightedZone;

        private void Awake() => DisHighlight();

        public Transform GetPlacePivot() => _placePivot;
        public void Highlight() => _highlightedZone.SetActive(true);
        public void DisHighlight() => _highlightedZone.SetActive(false);
    }
}
