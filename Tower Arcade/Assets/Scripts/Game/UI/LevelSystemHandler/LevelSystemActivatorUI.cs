using DG.Tweening;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class LevelSystemActivatorUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

        //Dependencies
        private LevelSystemSO _levelSystem;
        private EventBus _eventBus;
        private LevelConfigurationSO _levelConfiguration;
        private SoundHandler _soundHandler;

        private void Awake()
        {
            _levelSystem = LevelDI.Resolve<LevelSystemSO>();
            _eventBus = LevelDI.Resolve<EventBus>(); ;
            _levelConfiguration = LevelDI.Resolve<LevelConfigurationSO>();
            _soundHandler = LevelDI.Resolve<SoundHandler>();
        }

        private void Start()
        {
            _levelSystemActivatorRoot.SetActive(true);

            ConfigurateLevel(_levelSystem);

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
        public void OnPointerEnter(PointerEventData eventData)
        {
            _startLevelSystemButton.transform.DOScale(1.1f, 0.2f)
                .SetEase(Ease.Linear)
                .Play();

            _hideLevelInfoButton.transform.DOScale(1.1f, 0.2f)
                .SetEase(Ease.Linear)
                .Play();
                
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _startLevelSystemButton.transform.DOScale(1f, 0.2f)
                .SetEase(Ease.Linear)
                .Play();

            _hideLevelInfoButton.transform.DOScale(1f, 0.2f)
                .SetEase(Ease.Linear)
                .Play();
        }

        private void ConfigurateLevel(LevelSystemSO levelSystem)
        {
            _levelDifficultyText.text = $"Difficulty: {_levelConfiguration.GetLevelDifficulty()}";
            _wavesCountText.text = $"Waves: {levelSystem.GetWavesCount().ToString()}";
            _levelDescriptionText.text = _levelConfiguration.GetLevelDescrition();
        }

        private void TriggerHideLevelInfoAnimation()
        {
            _hideLevelInfoButton.onClick.RemoveAllListeners();

            _soundHandler.PlaySound(ClipName.Click);

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

            _soundHandler.PlaySound(ClipName.Click);

            _eventBus.Invoke(new OnLevelSystemStartedSignal());

            _startLevelSystemButton.GetComponent<RectTransform>().DOAnchorPosY(100f, 1f)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => _levelSystemActivatorRoot.gameObject.SetActive(false));
        }
    }
}
