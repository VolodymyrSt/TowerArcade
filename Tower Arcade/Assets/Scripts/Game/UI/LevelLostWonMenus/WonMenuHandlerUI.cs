using DG.Tweening;
using Sound;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class WonMenuHandlerUI : MonoBehaviour
    {
        [Header("Root")]
        [SerializeField] private RectTransform _movableRoot;

        [Header("Buttons")]
        [SerializeField] private Button _goToMenuButton;
        [SerializeField] private Button _exitButton;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _coinsAmountText;

        [Header("Stars")]
        [SerializeField] private RectTransform _starRoot;

        //Dependencies
        private HealthBarHandlerUI _healthBarHandler;
        private LevelSoundHandler _soundHandler;
        private LevelConfigurationSO _levelConfigurationSO;
        private CoinBalanceUI _coinBalanceUI;
        private SaveData _saveData;
        private SaveSystem _saveSystem;
        private LocationHandler _locationHandler;
        private LevelEntranceController _currentLevelEntranceController;

        private void Start()
        {
            SceneLoader sceneLoader = LevelDI.Resolve<SceneLoader>();
            TimeHandler timeHandler = LevelDI.Resolve<TimeHandler>();
            LevelDI.Resolve<EventBus>().SubscribeEvent<OnGameWonSignal>(ShowWonMenu);

            _levelConfigurationSO = LevelDI.Resolve<LevelConfigurationSO>();
            _healthBarHandler = LevelDI.Resolve<HealthBarHandlerUI>();
            _soundHandler = LevelDI.Resolve<LevelSoundHandler>();
            _coinBalanceUI = LevelDI.Resolve<CoinBalanceUI>();
            _saveData = LevelDI.Resolve<SaveData>();
            _saveSystem = LevelDI.Resolve<SaveSystem>();
            _locationHandler = LevelDI.Resolve<LocationHandler>();
            _currentLevelEntranceController = LevelDI.Resolve<LevelEntranceController>();

            InitButtons(sceneLoader, timeHandler);

            _coinsAmountText.text = _levelConfigurationSO.GetVictoryCoins().ToString();
            transform.gameObject.SetActive(false);
        }

        private void InitButtons(SceneLoader sceneLoader, TimeHandler timeHandler)
        {
            _goToMenuButton.onClick.AddListener(() =>
            {
                sceneLoader.LoadWithLoadingScene(SceneLoader.Scene.Menu);
            });

            _exitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

        private void ShowWonMenu(OnGameWonSignal signal)
        {
            float duration = 1f;

            transform.gameObject.SetActive(true);

            EvaluatePassing();

            _movableRoot.DOAnchorPosY(0, duration)
                .SetEase(Ease.Linear)
                .Play();

            _soundHandler.PlaySound(ClipName.Victory);

            _coinBalanceUI.IncreaseCoinBalace(_levelConfigurationSO.GetVictoryCoins());

            _saveData.CoinCurrency = _coinBalanceUI.GetCoinBalance();
            _saveSystem.Save(_saveData);

            if (_locationHandler.GetMaxEntranceCount() == _currentLevelEntranceController.GetEntranceIndex())
            {
                return;
            }
            else if(_currentLevelEntranceController.GetEntranceIndex() + 1 == _locationHandler.GetNextLockedEntrance().GetEntranceIndex())
            {
                _locationHandler.UnLockNextEntrance();
            }
        }

        private void EvaluatePassing()
        {
            var stars = GetStars();
            int starCount = 2;

            float health = _healthBarHandler.GetCurrentHealth();
            if (health >= 90f)
            {
                starCount = 0;
            }
            else if (health >= 75f)
            {
                starCount = 1;
            }

            FillStars(stars, starCount);
        }

        private void FillStars(List<StarSwitcher> stars, int countOfUnfilledStars)
        {
            for (int i = 0; i < stars.Count - countOfUnfilledStars; i++)
            {
                stars[i].SetStar(true);
            }
        }

        private List<StarSwitcher> GetStars()
        {
            List<StarSwitcher> starList = new List<StarSwitcher>();

            foreach (Transform star in _starRoot)
            {
                if (star.TryGetComponent(out StarSwitcher starSwitcher))
                {
                    starList.Add(starSwitcher);
                }
            }

            return starList;
        }
    }
}
