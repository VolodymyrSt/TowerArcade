using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LevelSystemActivatorUI : MonoBehaviour
    {
        [Header("Root")]
        [SerializeField] private GameObject _levelSystemActivatorRoot;

        [Header("Button")]
        [SerializeField] private Button _startLevelSystemButton;
        [SerializeField] private Button _hideLevelInfoButton;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _levelDifficultyText;
        [SerializeField] private TextMeshProUGUI _wavesCountText;
        [SerializeField] private TextMeshProUGUI _levelDescriptionText;

        private LevelDescriptionSO _levelDescriptionSO;
        private EventBus _eventBus;

        public void Initialize(LevelSystemSO levelSystem, EventBus eventBus, LevelDescriptionSO levelDescription)
        {
            _levelSystemActivatorRoot.gameObject.SetActive(true);

            _eventBus = eventBus;
            _levelDescriptionSO = levelDescription;

            LevelConfiguration(levelSystem);

            _startLevelSystemButton.onClick.AddListener(() => 
                StartLevelSystem()
            );

            _levelSystemActivatorRoot.GetComponent<RectTransform>().DOAnchorPosY(0f, 1f)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() =>
                {
                    _hideLevelInfoButton.onClick.AddListener(() => TriggerHideLevelInfoAnimation());
                });
        }

        private void LevelConfiguration(LevelSystemSO levelSystem)
        {
            _levelDifficultyText.text = $"Difficulty: {_levelDescriptionSO.GetLevelDifficulty()}";
            _wavesCountText.text = $"Waves: {levelSystem.GetWavesCount().ToString()}";
            _levelDescriptionText.text = _levelDescriptionSO.GetLevelDescrition();
        }

        private void TriggerHideLevelInfoAnimation()
        {
            _hideLevelInfoButton.onClick.RemoveAllListeners();

            _levelSystemActivatorRoot.GetComponent<RectTransform>().DOAnchorPosY(450f, 1f)
               .SetEase(Ease.Linear)
               .Play()
               .OnComplete(() =>
               {
                   TriggerStartLevelButtonAnimation();
               });
        }

        private void TriggerStartLevelButtonAnimation()
        {
            _startLevelSystemButton.GetComponent<RectTransform>().DOAnchorPosY(-450f, 1f)
                .SetEase(Ease.Linear)
                .Play();
        }

        private void StartLevelSystem()
        {
            _startLevelSystemButton.onClick.RemoveAllListeners();

            _eventBus.Invoke(new OnLevelSystemStartedSignal());

            _startLevelSystemButton.GetComponent<RectTransform>().DOAnchorPosY(100f, 1f)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => _levelSystemActivatorRoot.gameObject.SetActive(false));
        }
    }
}
