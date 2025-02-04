using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LevelEntranceController : MonoBehaviour
    {
        [SerializeField] private Button _enterButton;
        [SerializeField] private TextMeshProUGUI _levelNumber;

        [SerializeField] private Image _lockImage;

        [SerializeField] private List<RectTransform> _stars = new();

        private bool _isLocked = true;
        private bool _isEvaluated = false;

        public void Init(SceneLoader sceneLoader)
        {
            if (_isLocked)
                LockLevel();
            else
                UnlockLevel();

            _enterButton.onClick.AddListener(() => Enter(sceneLoader));

            if (!_isEvaluated)
                CalculateStars(3);
        }

        private void Enter(SceneLoader sceneLoader)
        {
            if(_isLocked) return;

            sceneLoader.LoadWithLoadingScene($"Level {_levelNumber.text.ToString()}");
        }

        public void UnlockLevel()
        {
            _isLocked = false;
            _lockImage.gameObject.SetActive(false);
        }
        
        private void LockLevel()
        {
            _isLocked = true;
            _lockImage.gameObject.SetActive(true);
        }

        public void CalculateStars(int numberOfUnfilledStars)
        {
            if (numberOfUnfilledStars < 0)
            {
                numberOfUnfilledStars = 0;
            }
            else if (numberOfUnfilledStars > _stars.Count)
            {
                numberOfUnfilledStars = _stars.Count;
            }

            int numberOfFilledStars = _stars.Count - numberOfUnfilledStars;

            for (int i = 0; i < numberOfFilledStars; i++)
            {
                _stars[i].gameObject.SetActive(true);
            }

            for (int i = numberOfFilledStars; i < _stars.Count; i++)
            {
                _stars[i].gameObject.SetActive(false);
            }
        }
    }
}
