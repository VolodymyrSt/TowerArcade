using DG.Tweening;
using DI;
using Sound;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SwipeHandler : MonoBehaviour
    {
        [Header("Content Viewport")]
        [SerializeField] private Image _contentDisplay;
        [SerializeField] private RectTransform _contentPosition;

        [Header("Location")]
        [SerializeField] private List<LocationData> _locationDatas = new();
        [SerializeField] private TextMeshProUGUI _currentLocationText;

        [Header("Pagination Buttons")]
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButton;

        [Header("Animation Settings")]
        [SerializeField] private Vector3 _offsetPosition;
        [SerializeField] private float _animationDuration = 1f;

        private SoundHandler _soundHandler;

        private int _currentIndex;
        private bool _isSwiped = false;

        public void Awake()
        {
            _currentIndex = 0;

            UpdateLocation();

            _nextButton.onClick.AddListener(NextContent);
            _previousButton.onClick.AddListener(PreviousContent);

            UpdateButtonStates();
        }

        private void Start() => _soundHandler = MenuDI.Resolve<SoundHandler>();

        private void NextContent()
        {
            if (_currentIndex < _locationDatas.Count - 1 && !_isSwiped)
            {
                _currentIndex++;

                MoveNextContent();
                UpdateButtonStates();

                _soundHandler.PlaySound(ClipName.SoftClick);
            }
        }

        private void PreviousContent()
        {
            if (_currentIndex > 0 && _isSwiped)
            {
                _currentIndex--;

                MovePreviousContent();
                UpdateButtonStates();

                _soundHandler.PlaySound(ClipName.SoftClick);
            }
        }

        private void MoveNextContent()
        {
            Vector2 newPosition = _contentPosition.anchoredPosition - new Vector2(_offsetPosition.x, 0);
            _contentPosition.DOAnchorPos(newPosition, _animationDuration)
                .SetEase(Ease.InOutCubic)
                .Play().OnComplete(() => { 
                    UpdateLocation();
                    _isSwiped = true;
                });
        }
        
        private void MovePreviousContent()
        {
            Vector2 newPosition = _contentPosition.anchoredPosition + new Vector2(_offsetPosition.x, 0);
            _contentPosition.DOAnchorPos(newPosition, _animationDuration)
                .SetEase(Ease.InOutCubic)
                .Play().OnComplete(() => {
                    UpdateLocation();
                    _isSwiped = false;
                });

        }

        private void UpdateButtonStates()
        {
            _previousButton.interactable = _currentIndex > 0;
            _nextButton.interactable = _currentIndex < _locationDatas.Count - 1;
        }

        private void UpdateLocation()
        {
            _currentLocationText.text = GetCurrentLocationData().GetLocationName();
        }

        public LocationData GetCurrentLocationData() => _locationDatas[_currentIndex];
        public int GetCurrentIndex() => _currentIndex;
    }
}
